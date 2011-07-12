namespace dk.CctalkLib.Devices
{
	public enum CctalkDeviceStatus:byte
	{
		// cctalk ref part3 page 72
		//0 OK 
		//1 Coin return mechanism activated ( flight deck open ) 
		//2 C.O.S. mechanism activated ( coin-on-string ) 

		Ok = 0,
		CoinReturn = 1,
		Cos = 2
	}
}