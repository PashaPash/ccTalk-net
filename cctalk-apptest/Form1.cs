using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using dk.CctalkLib.Connections;
using dk.CctalkLib.Devices;

namespace cctalk_apptest
{
	public partial class Form1 : Form
	{
		readonly CoinAcceptor _ca;
		Decimal _coinCounter = 0;

		public Form1()
		{
			InitializeComponent();

			var con = new ConnectionRs232
			          	{
			          		PortName = "com12",
			          	};



			_ca = new CoinAcceptor(
				14,
				con,
				new Dictionary<byte, CoinTypeInfo>
					{
						{5, new CoinTypeInfo("50kNew",0.5M)},
						{6, new CoinTypeInfo("1 R new",1M)},
						{7, new CoinTypeInfo("2 R new",2M)},
						{8, new CoinTypeInfo("5 R new",5M)},
						{9, new CoinTypeInfo("10 R new",10M)},

						{11, new CoinTypeInfo("50 kopec",0.5M)},
						{12, new CoinTypeInfo("1 rubles",1M)},
						{13, new CoinTypeInfo("2 rubles",2M)},
						{14, new CoinTypeInfo("5 rubles",5M)},
						{15, new CoinTypeInfo("10 ruble",10M)},
					},
				null,
				this
				);
			_ca.CoinAccepted += _ca_CoinAccepted;
			_ca.ErrorMessageAccepted += _ca_ErrorMessageAccepted;
		}

		void _ca_ErrorMessageAccepted(object sender, CoinAcceptorErrorEventArgs e)
		{
			listBox1.Items.Add(String.Format("Coin acceptor error: {0} ({1}, {2:X2})", e.ErrorMessage, e.Error, (Byte)e.Error));
		}

		void _ca_CoinAccepted(object sender, CoinAcceptorCoinEventArgs e)
		{
			_coinCounter += e.CoinValue;
			listBox1.Items.Add(String.Format("Coin accepted: {0} ({1:X2}), path {3}. Now accepted: {2:C}", e.CoinName, e.CoinCode, _coinCounter, e.RoutePath));

		}

		private void button1_Click(object sender, EventArgs e)
		{
			var con = new ConnectionRs232
			{
				PortName = "com12",
			};
			con.Open();

			var c = new GenericCctalkDevice
			        	{
			        		Connection = con,
							Address = 0
			        	};

			if (radioButton1.Checked)
			{
				var buf = c.CmdReadEventBuffer();

				var newEventsCount = _lastEvent <= buf.Counter ? buf.Counter - _lastEvent : (255 - _lastEvent) + buf.Counter;
				_lastEvent = buf.Counter;

				if (newEventsCount == 0)
				{
					listBox1.Items.Add("Нет новых событий");
				} else
				{
					var sb = new StringBuilder();
					sb.Append("Принято: ");
					for (int i = 0; i < Math.Min(newEventsCount, buf.Events.Length); i++)
					{
						var ev = buf.Events[i];
						sb.AppendFormat("({0:X2} {1:X2}) ", ev.CoinCode, ev.ErrorOrRouteCode);
					}

					var eventsLost = newEventsCount - buf.Events.Length;

					if (eventsLost > 0)
					{
						sb.AppendFormat(" Пропущено событий: {0}", eventsLost);
					}

					_lastEvent = buf.Counter;

					listBox1.Items.Add(sb.ToString());
				}

			} else if (radioButton2.Checked)
			{
				var serial = c.CmdGetSerial();
				listBox1.Items.Add(String.Format("SN: {0}", serial));

			} else if (radioButton3.Checked)
			{
				c.CmdReset();
				_lastEvent = 0;

			}


			//c.CmdReset();

			//listBox1.Items.Add(
			//    String.Format(
			//        "{0}=>{1}({2}) D:{3}",
			//        resp.SrcAdr,
			//        resp.DestAdr,
			//        resp.Header,
			//        ByteToAscii.Execute(resp.Data)
			//        )
			//    );


			con.Close();

		}

		Byte _lastEvent;

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
		}

		private void cbPolling_CheckedChanged(object sender, EventArgs e)
		{
			if(!_ca.IsInitialized)
				_ca.Init();

			if (cbPolling.Checked)
				_ca.StartPoll();
			else
				_ca.EndPoll();

			groupBox1.Enabled = !_ca.IsInitialized;

		}

		private void clearMoneyCounterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_coinCounter = 0;
		}

		private void cbInhibit_CheckedChanged(object sender, EventArgs e)
		{
			_ca.IsInhibiting = cbInhibit.Checked;
		}
	}
}
