using System;
using System.Threading;
using System.IO.Ports;
using CctalkLib.Devices;
using dk.CctalkLib.Checksumms;
using dk.CctalkLib.Devices;
using dk.CctalkLib.Messages;

namespace dk.CctalkLib.Connections
{
	public class ConnectionRs232 : ICctalkConnection
	{
		enum RespondAcceptionPhase
		{
			CommandNotSent,
			WaitingResponseStart,
			Accepting,
		}


		readonly SerialPort _port = new SerialPort();
		readonly Byte[] _respondBuf = new byte[255];
		readonly AutoResetEvent _readWait = new AutoResetEvent(false);
		readonly Object _callSyncRoot = new object();

		Int32 _respondBufPos;
		ICctalkChecksum _respondChecksumChecker;
		CctalkMessage _lastRespond;
		Int32 _lastByteReciveTimestamp;
		RespondAcceptionPhase _respondAcceptionPhase = RespondAcceptionPhase.CommandNotSent;


		public int BaudRate
		{
			get { return _port.BaudRate; }
			set { _port.BaudRate = value; }
		}

		public int DataBits
		{
			get { return _port.DataBits; }
			set { _port.DataBits = value; }
		}


		public Parity Parity
		{
			get { return _port.Parity; }
			set { _port.Parity = value; }
		}

		public StopBits StopBits
		{
			get { return _port.StopBits; }
			set { _port.StopBits = value; }
		}

		public Handshake Handshake
		{
			get { return _port.Handshake; }
			set { _port.Handshake = value; }
		}

		public String PortName
		{
			get { return _port.PortName; }
			set { _port.PortName = value; }
		}



		public ConnectionRs232()
		{
			SetDefaultPortConfig();

			_port.ReadBufferSize = 2048;
			_port.WriteBufferSize = 2048;
		}

		void SetDefaultPortConfig()
		{
			_port.Handshake = Handshake.None;
			_port.Parity = Parity.None;
			_port.PortName = "com1";
			_port.BaudRate = 9600;
			_port.StopBits = StopBits.One;
			_port.DataBits = 8;

		}

		#region Finalizing and disposing

		~ConnectionRs232()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			lock (_callSyncRoot)
			{
				Dispose(true);
			}
		}

		void Dispose(bool disposing)
		{
			_port.Dispose();
			//if (_serialPort.IsOpen) 
			//	_serialPort.Close();
		}

		#endregion

		#region IConnection members

		public bool IsOpen()
		{
			return _port.IsOpen;
		}

		public void Open()
		{
			lock (_callSyncRoot)
			{
				_port.DataReceived += SerialPortDataReceived;
				_port.Open();
				IsOpen();
			}
		}

		public void Close()
		{
			lock (_callSyncRoot)
			{
				_port.DataReceived -= SerialPortDataReceived;
				_port.Close();
			}
		}


		public CctalkMessage Send(CctalkMessage com, ICctalkChecksum chHandler)
		{
			// TODO: handle BUSY message

			lock (_callSyncRoot)
			{
				if (_respondAcceptionPhase != RespondAcceptionPhase.CommandNotSent)
					throw new InvalidOperationException();

				var msgBytes = com.GetTransferDataNoChecksumm();
				chHandler.CalcAndApply(msgBytes);


				_lastRespond = null;
				_respondChecksumChecker = chHandler;


				_port.Write(msgBytes, 0, msgBytes.Length);

				_respondAcceptionPhase = RespondAcceptionPhase.WaitingResponseStart;
				SetTimestamp();
				while (!_readWait.WaitOne(50))
				{
					/*
					 * When receiving bytes within a message packet, the communication software should 
					 * wait up to 50ms for another byte if it is expected. If a timeout condition occurs, the 
					 * software should reset all communication variables and be ready to receive the next 
					 * message. No other action should be taken. (cctalk spec part1, 11.1) 
					 */

					var tsAge = GetTimestampAge();
					switch (_respondAcceptionPhase)
					{
						case RespondAcceptionPhase.WaitingResponseStart:
							if (tsAge > 2000) throw new TimeoutException("No reply");
							break;
						case RespondAcceptionPhase.Accepting:
							if (tsAge > 50) throw new TimeoutException("Pause in reply");
							break;


					}
					if (tsAge > 50) throw new TimeoutException("No reply");
				}

				_respondChecksumChecker = null;
				_respondAcceptionPhase = RespondAcceptionPhase.CommandNotSent;

				return _lastRespond;
			}
		}

		void SetTimestamp()
		{
			_lastByteReciveTimestamp = Environment.TickCount;
		}

		Int32 GetTimestampAge()
		{
			return Environment.TickCount - _lastByteReciveTimestamp;
		}

		private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			_respondAcceptionPhase = RespondAcceptionPhase.Accepting;

			int bytes = _port.BytesToRead;
			var comBuffer = new byte[bytes];
			var red = _port.Read(comBuffer, 0, bytes);
			Array.Copy(comBuffer, 0, _respondBuf, _respondBufPos, red);
			_respondBufPos += red;
			SetTimestamp();
			var isRespondComplete = GenericCctalkDevice.IsRespondComplete(_respondBuf, _respondBufPos);
			if (isRespondComplete)
			{
				if (!_respondChecksumChecker.Check(_respondBuf, 0, _respondBufPos))
				{
					var copy = new byte[_respondBufPos];
					Array.Copy(_respondBuf, copy, _respondBufPos);
					throw new InvalidResondFormatException(copy, "Checksumm check fail");
				}
				_lastRespond = GenericCctalkDevice.ParseRespond(_respondBuf, 0, _respondBufPos);
				Array.Clear(_respondBuf, 0, _respondBuf.Length);
				_respondBufPos = 0;
				_readWait.Set();
			}
		}



		#endregion

	}
}