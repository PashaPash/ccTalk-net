using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using dk.CctalkLib.Connections;
using dk.CctalkLib.Devices;

namespace CctalkLib.Devices
{

	/// <summary>
	/// Encapsulates various routines linked with coin acceptors. 
	/// Also invokes events to Wpf or Windows.Forms application/
	/// </summary>
	public class CoinAcceptor : IDisposable
	{
		const Int32 PollPeriod = 1000;

		readonly GenericCctalkDevice _rawDev = new GenericCctalkDevice();
		readonly ISynchronizeInvoke _winformsInvokeTarget;
		readonly DispatcherObject _wpfInvokeTarget;

		Timer _t;
		Byte _lastEvent;

		public event EventHandler<CoinAcceptorCoinEventArgs> CoinAccepted;
		public event EventHandler<CoinAcceptorErrorEventArgs> ErrorMessageAccepted;

		TimeSpan _pollInterval;


		readonly Object _timersSyncRoot = new Object(); // for sync with timer threads only
		readonly Dictionary<Byte, String> _errors = new Dictionary<byte, string>();
		readonly Dictionary<Byte, CoinTypeInfo> _coins = new Dictionary<Byte, CoinTypeInfo>();

		#region Constructors
		CoinAcceptor(
			Byte addr,
			ICctalkConnection configuredConnection,
			Dictionary<Byte, CoinTypeInfo> coins,
			Dictionary<Byte, String> errorNames,
			ISynchronizeInvoke winformsInvokeTarget,
			DispatcherObject wpfInvokeTarget
			)
		{
			if (configuredConnection == null) throw new ArgumentNullException("configuredConnection");

			_rawDev.Connection = configuredConnection;
			_rawDev.Address = addr;
			_winformsInvokeTarget = winformsInvokeTarget;
			_wpfInvokeTarget = wpfInvokeTarget;

			if (errorNames != null)
				foreach (var error in errorNames)
					_errors[error.Key] = error.Value;

			if (coins != null)
				foreach (var coin in coins)
					_coins[coin.Key] = coin.Value;
		}

		/// <summary>
		/// Creates instance of CoinAcceptor and sets up WinForms invoking
		/// </summary>
		/// <param name="addr">Device ccTalk address. 0 - broadcast</param>
		/// <param name="configuredConnection">Connection to work. Sharing connection between seweral device object not recommended and not supported. But can work :)</param>
		/// <param name="coins">data for resolving coin codes to coin values and names. Depends on device firmware. Can be found on device case.</param>
		/// <param name="errorNames">overriding for error nemes</param>
		/// <param name="winformsInvokeTarget">Form or other Windows.Forms control, that wil handle invoking into WinForms App</param>
		public CoinAcceptor(
			Byte addr,
			ICctalkConnection configuredConnection,
			Dictionary<Byte, CoinTypeInfo> coins,
			Dictionary<Byte, String> errorNames,
			ISynchronizeInvoke winformsInvokeTarget
			)
			: this(addr, configuredConnection, coins, errorNames, winformsInvokeTarget, null)
		{
		}

		/// <summary>
		/// Creates instance of CoinAcceptor and sets up WPF invoking
		/// </summary>
		/// <param name="addr">Device ccTalk address. 0 - broadcast</param>
		/// <param name="configuredConnection">Connection to work. Sharing connection between seweral device object not recommended and not supported. But can work :)</param>
		/// <param name="coins">data for resolving coin codes to coin values and names. Depends on device firmware. Can be found on device case.</param>
		/// <param name="errorNames">overriding for error nemes</param>
		/// <param name="wpfInvokeTarget">WPF control, which will handle WPF invoking</param>
		public CoinAcceptor(
			Byte addr,
			ICctalkConnection configuredConnection,
			Dictionary<Byte, CoinTypeInfo> coins,
			Dictionary<Byte, String> errorNames,
			DispatcherObject wpfInvokeTarget
			)
			: this(addr, configuredConnection, coins, errorNames, null, wpfInvokeTarget)
		{
		}

		/// <summary>
		/// Creates instance of CoinAcceptor without invoking
		/// </summary>
		/// <param name="addr">Device ccTalk address. 0 - broadcast</param>
		/// <param name="configuredConnection">Connection to work. Sharing connection between seweral device object not recommended and not supported. But can work :)</param>
		/// <param name="coins">data for resolving coin codes to coin values and names. Depends on device firmware. Can be found on device case.</param>
		/// <param name="errorNames">overriding for error nemes</param>
		public CoinAcceptor(
			Byte addr,
			ICctalkConnection configuredConnection,
			Dictionary<Byte, CoinTypeInfo> coins,
			Dictionary<Byte, String> errorNames
			)
			: this(addr, configuredConnection, coins, errorNames, null, null)
		{
		}

		/// <summary>
		/// Creates instance of CoinAcceptor with minimal custumization
		/// </summary>
		/// <param name="addr">Device ccTalk address. 0 - broadcast</param>
		/// <param name="configuredConnection">Connection to work. Sharing connection between seweral device object not recommended and not supported. But can work :)</param>
		public CoinAcceptor(
			Byte addr,
			ICctalkConnection configuredConnection
			)
			: this(addr, configuredConnection, null, null, null, null)
		{
		}
		#endregion

		/// <summary>
		///  Opens connection and request main data from device (e.g. Serial number)
		/// </summary>
		public void Init()
		{
			_rawDev.Connection.Open();

			DeviceCategory = _rawDev.CmdRequestEquipmentCategory();
			if (DeviceCategory != CctalkDeviceTypes.CoinAcceptor)
				throw new InvalidOperationException("Connected device is not a coin acceptor");

			_rawDev.CmdReset();
			_rawDev.CmdSetMasterInhibitStatus(IsInhibiting);

			SerialNumber = _rawDev.CmdGetSerial();
			PollInterval = _rawDev.CmdRequestPollingPriority();
			Manufacturer = _rawDev.CmdRequestManufacturerId();
			ProductCode = _rawDev.CmdRequestProductCode();

			IsInitialized = true;
		}


		public void UnInit()
		{
			lock (_timersSyncRoot)
			{
				EndPoll();
				_rawDev.Connection.Close();
				IsInitialized = false;
			}

		}


		public Boolean IsInitialized { get; private set; }

		/// <summary>
		///  ccTalk address of device. 0 - broadcast.
		/// </summary>
		public Byte Address { get { return _rawDev.Address; } }
		public Boolean IsPolling { get { return _t != null; } }
		protected String ProductCode { get; private set; }

		/// <summary>
		/// Serial number of device. Value accepted from device while Init.
		/// </summary>
		public Int32 SerialNumber { get; private set; }

		public String Manufacturer { get; private set; }
		public CctalkDeviceTypes DeviceCategory { get; private set; }


		bool _isInhibiting;
		public Boolean IsInhibiting
		{
			get { return _isInhibiting; }
			set
			{
				_rawDev.CmdSetMasterInhibitStatus(value);
				_isInhibiting = value;
			}

		}



		/// <summary>
		/// *CURRENTLY UNUSED: interval hardcoded*
		/// Poll interval. By default value accepted from device while Init procedure. Can be changed, but next Init will reset value.
		/// If value = TimeSpan.Zero - device not recommends any intervals or use different hardware lines for polling.
		/// </summary>
		public TimeSpan PollInterval
		{
			get { return _pollInterval; }
			set
			{
				if (IsPolling)
					throw new InvalidOperationException("Stop polling first");
				_pollInterval = value;
			}
		}



		/// <summary>
		///  Starts poll events from device
		/// </summary>
		public void StartPoll()
		{
			if (_t != null)
				throw new InvalidOperationException("Stop polling first");

			lock (_timersSyncRoot)
			{
				if (!_rawDev.Connection.IsOpen())
					throw new InvalidOperationException("Init first");
				_t = new Timer(TimerTick, _rawDev, 50, PollPeriod);
			}
		}

		/// <summary>
		///  Stops poll events from device
		/// </summary>
		public void EndPoll()
		{
			if (_t == null) return;
			lock (_timersSyncRoot)
			{
				_t.Dispose();
				_t = null;
			}
		}


		/// <summary>
		///  Polls the device immediatly. Returns true if device is ready. 
		///  There is no need in calling this method when devise is polled.
		/// </summary>
		public bool IsReady
		{
			get
			{
				if (!IsInitialized) return false;
				var status = _rawDev.CmdRequestStatus();
				return status == CctalkDeviceStatus.Ok;
			}
		}


		void TimerTick(object state)
		{
			lock (_timersSyncRoot)
			{
				var buf = _rawDev.CmdReadEventBuffer();

				var newEventsCount = _lastEvent <= buf.Counter ? buf.Counter - _lastEvent : (255 - _lastEvent) + buf.Counter;
				_lastEvent = buf.Counter;

				if (newEventsCount != 0)
				{
					for (int i = 0; i < Math.Min(newEventsCount, buf.Events.Length); i++)
					{
						var ev = buf.Events[i];
						if (ev.IsError)
						{
							String errMsg;
							var errCode = (CoinAcceptorErrors)ev.ErrorOrRouteCode;
							_errors.TryGetValue(ev.ErrorOrRouteCode, out errMsg);
							BeginInvokeErrorEvent(new CoinAcceptorErrorEventArgs(errCode, errMsg));

						} else
						{
							CoinTypeInfo coinInfo;
							_coins.TryGetValue(ev.CoinCode, out coinInfo);
							var evVal = coinInfo == null ? 0 : coinInfo.Value;
							var evName = coinInfo == null ? null : coinInfo.Name;
							BeginInvokeCoinEvent(new CoinAcceptorCoinEventArgs(evName, evVal, ev.CoinCode, ev.ErrorOrRouteCode));
						}
					}

					var eventsLost = newEventsCount - buf.Events.Length;

					if (eventsLost > 0)
					{
						BeginInvokeErrorEvent(new CoinAcceptorErrorEventArgs(CoinAcceptorErrors.UnspecifiedAlarmCode,
																			 "Events lost:" + eventsLost));
					}

					_lastEvent = buf.Counter;
				}
				// TODO: signal unexpected device reset, when device`s event cointer go to 0
			}


		}

		void BeginInvokeErrorEvent(CoinAcceptorErrorEventArgs ea)
		{
			BeginInvokeEvent(ErrorMessageAccepted, ea);

		}


		void BeginInvokeCoinEvent(CoinAcceptorCoinEventArgs ea)
		{
			BeginInvokeEvent(CoinAccepted, ea);
		}


		void BeginInvokeEvent(Delegate ev, EventArgs ea)
		{
			if (ev == null) return;

			bool needDirectCall = false;
			if (_winformsInvokeTarget != null)
			{
				if (_winformsInvokeTarget.InvokeRequired)
				{
					_winformsInvokeTarget.BeginInvoke(ev, new Object[] { this, ea });
				} else
					needDirectCall = true;
			}

			if (_wpfInvokeTarget != null)
			{
				if (!_wpfInvokeTarget.CheckAccess())
				{
					_wpfInvokeTarget.Dispatcher.BeginInvoke(ev, this, ea);
				} else
					needDirectCall = true;
			}

			if (needDirectCall)
				ev.DynamicInvoke(this, ea);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		public void Dispose(Boolean disposing)
		{
			UnInit();
		}

	}
}