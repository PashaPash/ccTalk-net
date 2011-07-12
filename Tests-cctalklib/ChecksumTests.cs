using System;
using dk.CctalkLib.Checksumms;
using NUnit.Framework;

namespace Tests_cctalklib
{
	[TestFixture]
	public class ChecksumTests
	{
		[Test]
		public void ChecksummRoundtip()
		{

			var b = new byte[255];
			var r = new Random();
			r.NextBytes(b);

			var c = new Checksum();

			b[254] = 0;
			c.CalcAndApply(b);

			Assert.IsTrue(c.Check(b, 0, b.Length));
		}

		[Test]
		public void ChecksummDoubleApply()
		{
			var b = new Byte[] { 1, 2, 3, 4, 5 };

			var c = new Checksum();

			try
			{
				c.CalcAndApply(b);
				Assert.Fail("Error expected");
			} catch (ArgumentException) { }

			b[4] = 0;
			c.CalcAndApply(b);
			Assert.AreEqual(246, b[4]);

			try
			{
				c.CalcAndApply(b);
				Assert.Fail("Error expected");
			} catch (ArgumentException) { }

			Assert.IsTrue(c.Check(b, 0, b.Length));
		}


	}
}
