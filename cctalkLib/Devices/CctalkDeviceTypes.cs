namespace dk.CctalkLib.Devices
{
	public enum CctalkDeviceTypes
	{
		Unknown = 0,
		CoinAcceptor = 2,//a.k.a Coin Validator 
		Payout = 3,//a.k.a Hopper 
		Reel = 30,
		BillValidator = 40,//a.k.a Note_Acceptor 
		CardReader = 50,
		Changer = 55,//Money-in, money-out recyclers. Also used for coin singulators and sorters. 
		Display = 60,// e.g. LCD panels, alpha-numeric displays 
		Keypad = 70,//Remote keyboard
		Dongle = 80,// Security device, interface box or interface hub 
		Meter = 90,//Electro-mechanical counter replacement 
		Bootloader = 99,//Bootloader firmware and diagnostics when no application code is loaded.
		Power = 100,// Power switching hub or intelligent power supply 
		Printer = 110,//Ticket printer for coupons and barcodes 
		RNG = 120,//Random Number Generator 
		HopperScale = 130,// Hopper with weigh scale 
		CoinFeeder = 140,//Motorised coin feeder or singulator
		Debug = 240,//This address range may be used when developing new peripherals 
	}
}