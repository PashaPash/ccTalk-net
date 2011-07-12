using System;
using dk.CctalkLib.Checksumms;
using dk.CctalkLib.Connections;
using dk.CctalkLib.Messages;

namespace Tests_cctalklib
{
	public class VirtualConnection:ICctalkConnection
	{
		protected Boolean IsOpened { get; set; }


		public void Dispose()
		{
			IsOpened = false;
		}

		public void Open()
		{
			IsOpened = true;

		}

		public bool IsOpen()
		{
			throw new NotImplementedException();
		}

		public CctalkMessage Send(CctalkMessage com, ICctalkChecksum checksumHandler)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			IsOpened = false;
		}

	}
}