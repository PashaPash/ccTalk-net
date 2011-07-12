using System;
using dk.CctalkLib.Messages;

namespace dk.CctalkLib.Devices
{
	[Serializable]
	internal class InvalidResondException : Exception
	{
		public InvalidResondException(CctalkMessage respond)
			: this(respond, "Invalid respond")
		{
		}

		public InvalidResondException(CctalkMessage respond, string message)
			: base(message)
		{
			InvalidRespond = respond;
		}

		public CctalkMessage InvalidRespond { get; private set; }

	}
}