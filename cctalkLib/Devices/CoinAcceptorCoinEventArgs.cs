using System;

namespace dk.CctalkLib.Devices
{
	public class CoinAcceptorCoinEventArgs : EventArgs
	{
		public String CoinName { get; private set; }
		public Decimal CoinValue { get; private set; }
		public Byte CoinCode { get; private set; }
		public Byte RoutePath { get; private set; }

		public CoinAcceptorCoinEventArgs(String coinName, Decimal coinValue, Byte coinCode, Byte routePath)
		{
			CoinName = coinName;
			CoinValue = coinValue;
			CoinCode = coinCode;
			RoutePath = routePath;
		}
	}
}