using System.Collections.Generic;
using dk.CctalkLib.Devices;
using NUnit.Framework;

namespace Tests_cctalklib
{
	[TestFixture]
	public class CoinAcceptorTests
	{
		[Test]
		public void ConfigWordTest()
		{
			var cnf = CoinAcceptor.ConfigWord(CoinAcceptor.DefaultConfig);
			Dictionary<byte, CoinTypeInfo> coins;
			Assert.That(CoinAcceptor.TryParseConfigWord(cnf, out coins));

			foreach (var coin in coins)
			{
				Assert.AreEqual(CoinAcceptor.DefaultConfig[coin.Key].Name, coin.Value.Name);
				Assert.AreEqual(CoinAcceptor.DefaultConfig[coin.Key].Value, coin.Value.Value);
			}
		}
	}
}
