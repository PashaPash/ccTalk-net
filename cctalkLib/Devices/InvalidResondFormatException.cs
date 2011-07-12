using System;

namespace dk.CctalkLib.Devices
{
	[Serializable]
	internal class InvalidResondFormatException : Exception
	{
		public InvalidResondFormatException(Byte[] respondRawData) : this(respondRawData, "Invalid respond")
		{
		}

		public InvalidResondFormatException(byte[] respondRawData, string message) : base(message)
		{
			InvalidRespondData = respondRawData;
		}

		public Byte[] InvalidRespondData { get; private set; }

	}
}