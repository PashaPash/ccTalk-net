using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using dk.CctalkLib.Connections;
using dk.CctalkLib.Devices;

namespace cctalk_apptest
{

	/// <summary>
	///  This form demonstrates usage of money acceptors through cctalk API
	/// </summary>
	public partial class Form1 : Form
	{
		CoinAcceptor _coinAcceptor;
		BillValidator _billValidator;

		Decimal _coinCounter; // counts accepted money amount in coins
		Decimal _notesCounter;// counts accepted money amount in bills

		public Form1()
		{
			InitializeComponent();

			// Showing message about current config for defices
			var configMessage = String.Format("Coin config:{0}{1}{0}Bill config:{0}{2}",
			                                  Environment.NewLine,
			                                  CoinAcceptor.ConfigWord(CoinAcceptor.DefaultConfig),
			                                  BillValidator.ConfigWord(BillValidator.DefaultConfig));

			configWord.Text = configMessage;

		}

		#region Device managment helpers

		private void TryCreateCoinAcceptor()
		{
			try
			{
				CreateCoinAcceptor();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				DisposeCoinAcceptor();

				////5-10 seconds device can be "Unusable"
				//Thread.Sleep(5000);
				//CreateCoinAcceptor();
			}


		}

		private void TryCreateBillValidator()
		{
			try
			{
				CreateBillValidator();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				DisposeBillValidator();

				////5-10 srconds device can be "Unusable"
				//Thread.Sleep(5000);
				//CreateCoinAcceptor();
			}

		}


		private void CreateBillValidator()
		{
			var con = new ConnectionRs232
						{
							PortName = GetCom(),
							RemoveEcho = true // if we are connected to USB-COM echo is present otherwise set to false
						};

			Dictionary<byte, BillTypeInfo> notes;
			if (!BillValidator.TryParseConfigWord(configWord.Text, out notes))
			{
				MessageBox.Show("Wrong config word, using defaults");

				notes = BillValidator.DefaultConfig;
				configWord.Text = BillValidator.ConfigWord(BillValidator.DefaultConfig);
			}

			_billValidator = new BillValidator(Convert.ToByte(deviceNumber.Value), con, notes, null);

			_billValidator.NotesAccepted += BillValidatorNotesAccepted;
			_billValidator.ErrorMessageAccepted += BillValidatorErrorMessageAccepted;

			_billValidator.Init();

			groupBox1.Enabled = true;
			panel1.Enabled = true;

			initCoinButton.Enabled = false;
			resetButton.Enabled = true;
			configWord.Enabled = false;
		}

		private void CreateCoinAcceptor()
		{
			var con = new ConnectionRs232
						{
							PortName = GetCom(),
						};

			Dictionary<byte, CoinTypeInfo> coins;
			if (!CoinAcceptor.TryParseConfigWord(configWord.Text, out coins))
			{
				MessageBox.Show("Wrong config word, using defaults");

				coins = CoinAcceptor.DefaultConfig;
				configWord.Text = CoinAcceptor.ConfigWord(CoinAcceptor.DefaultConfig);
			}

			_coinAcceptor = new CoinAcceptor(Convert.ToByte(deviceNumber.Value), con, coins, null);

			_coinAcceptor.CoinAccepted += CoinAcceptorCoinAccepted;
			_coinAcceptor.ErrorMessageAccepted += CoinAcceptorErrorMessageAccepted;

			_coinAcceptor.Init();

			groupBox1.Enabled = true;
			panel1.Enabled = true;

			initCoinButton.Enabled = false;
			resetButton.Enabled = true;
			configWord.Enabled = false;
		}

		private void DisposeCoinAcceptor()
		{
			if (_coinAcceptor == null)
				return;

			if (_coinAcceptor.IsInitialized)
			{
				_coinAcceptor.IsInhibiting = true;
				_coinAcceptor.UnInit();
			}

			_coinAcceptor.Dispose();

			_coinAcceptor = null;

			groupBox1.Enabled = false;
			panel1.Enabled = false;
			initCoinButton.Enabled = true;
			resetButton.Enabled = false;
			configWord.Enabled = true;
		}

		private void DisposeBillValidator()
		{
			if (_billValidator == null)
				return;

			if (_billValidator.IsInitialized)
			{
				_billValidator.IsInhibiting = true;
				_billValidator.UnInit();
			}

			_billValidator.Dispose();

			_billValidator = null;

			groupBox1.Enabled = false;
			panel1.Enabled = false;
			initCoinButton.Enabled = true;
			resetButton.Enabled = false;
			configWord.Enabled = true;
		}

		# endregion

		private string GetCom()
		{
			return string.Format("com{0:g0}", comNumber.Value);
		}

		void CoinAcceptorErrorMessageAccepted(object sender, CoinAcceptorErrorEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke((EventHandler<CoinAcceptorErrorEventArgs>)CoinAcceptorErrorMessageAccepted, sender, e);
				return;
			}

			listBox1.Items.Add(String.Format("Coin acceptor error: {0} ({1}, {2:X2})", e.ErrorMessage, e.Error, (Byte)e.Error));

			listBox1.SelectedIndex = listBox1.Items.Count - 1;
		}


		void BillValidatorErrorMessageAccepted(object sender, BillValidatorErrorEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke((EventHandler<BillValidatorErrorEventArgs>)BillValidatorErrorMessageAccepted, sender, e);
				return;
			}

			listBox1.Items.Add(String.Format("Notes acceptor error: {0} ({1}, {2:X2})", e.ErrorMessage, e.Error, (Byte)e.Error));

			listBox1.SelectedIndex = listBox1.Items.Count - 1;
			//listBox1.SelectedIndex = -1;
		}




		void CoinAcceptorCoinAccepted(object sender, CoinAcceptorCoinEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke((EventHandler<CoinAcceptorCoinEventArgs>)CoinAcceptorCoinAccepted, sender, e);
				return;
			}
			_coinCounter += e.CoinValue;
			listBox1.Items.Add(String.Format("Coin accepted: {0} ({1:X2}), path {3}. Now accepted: {2:C}", e.CoinName, e.CoinCode, _coinCounter, e.RoutePath));

			listBox1.SelectedIndex = listBox1.Items.Count - 1;
			//listBox1.SelectedIndex = -1;

			// There is simulator of long-working event handler
			Thread.Sleep(1000);
		}


		void BillValidatorNotesAccepted(object sender, BillValidatorBillEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke((EventHandler<BillValidatorBillEventArgs>)BillValidatorNotesAccepted, sender, e);
				return;
			}
			_notesCounter += e.NoteValue;
			listBox1.Items.Add(String.Format("Bill accepted: {0} ({1:X2}), path {3}. Now accepted: {2:C}", e.NoteName, e.NoteCode, _notesCounter, e.RoutePath));

			listBox1.SelectedIndex = listBox1.Items.Count - 1;
			//listBox1.SelectedIndex = -1;

			// There is simulator of long-working event handler
			Thread.Sleep(1000);
		}

		private void SendCommandButtonClick(object sender, EventArgs e)
		{
			// Attention! There we are creating new device object. But it could share connection with _coinAcceptor.
			ICctalkConnection con;
			Boolean isMyConnection;

			if (_coinAcceptor.Connection.IsOpen())
			{
				con = _coinAcceptor.Connection;
				isMyConnection = false;
			}
			else
			{
				con = new ConnectionRs232
				{
					PortName = GetCom(),
					RemoveEcho = true      // if we are connected to USB-COM echo is present otherwise set to false
				};
				con.Open();
				isMyConnection = true;
			}
			try
			{
				var c = new GenericCctalkDevice
							{
								Connection = con,
								Address = 0
							};

				if (radioButton1.Checked)
				{
					var buf = c.CmdReadEventBuffer();


					var sb = new StringBuilder();
					sb.Append("Accepted: ");
					sb.AppendFormat("Cntr={0} Data:", buf.Counter);
					for (int i = 0; i < buf.Events.Length; i++)
					{
						var ev = buf.Events[i];
						sb.AppendFormat("({0:X2} {1:X2}) ", ev.CoinCode, ev.ErrorOrRouteCode);
					}

					listBox1.Items.Add(sb.ToString());
					listBox1.SelectedIndex = listBox1.Items.Count - 1;


				}
				else if (radioButton2.Checked)
				{
					var serial = c.CmdGetSerial();
					listBox1.Items.Add(String.Format("SN: {0}", serial));
					listBox1.SelectedIndex = listBox1.Items.Count - 1;


				}
				else if (radioButton3.Checked)
				{
					c.CmdReset();
				}
			}
			finally
			{
				if (isMyConnection)
					con.Close();
			}

		}


		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
		}

		private void cbPolling_CheckedChanged(object sender, EventArgs e)
		{
			if (_billValidator == null) return;

			if (!_billValidator.IsInitialized)
				_billValidator.Init();

			if (cbPolling.Checked)
				_billValidator.StartPoll();
			else
				_billValidator.EndPoll();

			//groupBox1.Enabled = !_coinAcceptor.IsPolling;

		}

		private void clearMoneyCounterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_coinCounter = 0;
			_notesCounter = 0;
		}

		private void cbInhibit_CheckedChanged(object sender, EventArgs e)
		{
			if (_billValidator != null)
				_billValidator.IsInhibiting = cbInhibit.Checked;

			if (_coinAcceptor != null)
				_coinAcceptor.IsInhibiting = cbInhibit.Checked;

		}

		private void initBillButton_Click(object sender, EventArgs e)
		{
			try
			{
				TryCreateBillValidator();
			}
			catch (Exception ex)
			{

				DisposeBillValidator();
				MessageBox.Show(ex.Message, "Error while connecting bill validator", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void initCoinButton_Click(object sender, EventArgs e)
		{
			try
			{
				TryCreateCoinAcceptor();
			}
			catch (Exception ex)
			{

				DisposeCoinAcceptor();
				MessageBox.Show(ex.Message, "Error while connecting coin acceptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ResetButtonClick(object sender, EventArgs e)
		{
			_brutimer.Stop();

			DisposeCoinAcceptor();
			DisposeBillValidator();
			cbPolling.Checked = false;

		}

		private void ReadyButtonClick(object sender, EventArgs e)
		{
			var message = String.Format(
				"Coin acceptor status:{0}{1}{0}" +
				"Bill validator status:{0}{2}",
				Environment.NewLine,
				_coinAcceptor == null ? "not inited" : _coinAcceptor.GetStatus().ToString(),
				_billValidator == null ? "not inited" : _billValidator.GetStatus().ToString()
				);

			MessageBox.Show(message, Text);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		readonly System.Windows.Forms.Timer _brutimer = new System.Windows.Forms.Timer();

		private void cbBrute_CheckedChanged(object sender, EventArgs e)
		{
			// Let`s send many requests to device.
			if (cbBrute.Checked)
			{
				_brutimer.Interval = 1;
				_brutimer.Start();
				_brutimer.Tick += _brutimer_Tick;
			}
			else
			{
				_brutimer.Stop();
			}
		}

		void _brutimer_Tick(object sender, EventArgs e)
		{
			SendCommandButtonClick(sender, e);
		}

		private void butPollNow_Click(object sender, EventArgs e)
		{
			if (_coinAcceptor != null)
				_coinAcceptor.PollNow();

			if (_billValidator != null)
				_billValidator.PollNow();
		}


	}
}
