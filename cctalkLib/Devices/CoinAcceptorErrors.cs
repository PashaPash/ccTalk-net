namespace dk.CctalkLib.Devices
{
	public enum CoinAcceptorErrors
	{

		/*Null event ( no error ) */
		NoError = 0,
		/*Reject coin */
		RejectCoin = 1,
		/*Inhibited coin */
		InhibitedCoin = 2,
		/*Multiple window */
		MultipleWindow = 3,
		/*Wake-up timeout */
		WakeupTimeout = 4,
		/*Validation timeout */
		ValidationTimeout = 5,
		/*Credit sensor timeout */
		CreditSensorTimeout = 6,
		/*Sorter opto timeout */
		SorterOptoTimeout = 7,
		/*2nd close coin error */
		SecondCloseCoinError = 8,
		/*Accept gate not ready */
		AcceptGateNotReady = 9,
		/*Credit sensor not ready */
		CreditSensorNotReady = 10,
		/*Sorter not ready */
		SorterNotReady = 11,
		/*Reject coin not cleared */
		RejectCoinNotCleared = 12,
		/*Validation sensor not ready */
		ValidationSensorNotReady = 13,
		/*Credit sensor blocked */
		CreditSensorBlocked = 14,
		/*Sorter opto blocked */
		SorterOptoBlocked = 15,
		/*Credit sequence error */
		CreditSequenceError = 16,
		/*Coin going backwards */
		CoinGoingBackwards = 17,
		/*Coin too fast ( over credit sensor ) */
		CoinTooFast = 18,
		/*Coin too slow ( over credit sensor ) */
		CoinTooSlow = 19,
		/*C.O.S. mechanism activated ( coin-on-string ) */
		CosActivated = 20,
		/*DCE opto timeout */
		DceOptoTimeout = 21,
		/*DCE opto not seen */
		DceOptoNotSeen = 22,
		/*Credit sensor reached too early */
		CreditSensorReachedTooEarly = 23,
		/*Reject coin ( repeated sequential trip ) */
		RejectCoinRepeat = 24,
		/*Reject slug */
		RejectSlug = 25,
		/*Reject sensor blocked */
		RejectSensorBlocked = 26,
		/*Games overload */
		GamesOverload = 27,
		/*Max. coin meter pulses exceeded */
		MaxCoinMeterPulsesExceeded = 28,
		/*Accept gate open not closed */
		AcceptGateOpenNotClosed = 29,
		/*Accept gate closed not open */
		AcceptGateClosedNotOpen = 30,
		/*Manifold opto timeout */
		ManifoldOptoTimeout = 31,
		/*Manifold opto blocked */
		ManifoldOptoBlocked = 32,
		/*Manifold not ready */
		ManifoldNotReady = 33,
		/*Security status changed */
		SecurityStatusChanged = 34,
		/*Motor exception */
		MotorException = 35,
		/*Inhibited coin ( Type 1 ) */
		InhibitedCoin01 = 128,
		/*Inhibited coin ( Type 2 ) */
		InhibitedCoin02 = 129,
		/*Inhibited coin ( Type 3 ) */
		InhibitedCoin03 = 130,
		/*Inhibited coin ( Type 4 ) */
		InhibitedCoin04 = 131,
		/*Inhibited coin ( Type 5 ) */
		InhibitedCoin05 = 132,
		/*Inhibited coin ( Type 6 ) */
		InhibitedCoin06 = 133,
		/*Inhibited coin ( Type 7 ) */
		InhibitedCoin07 = 134,
		/*Inhibited coin ( Type 8 ) */
		InhibitedCoin08 = 135,
		/*Inhibited coin ( Type 9 ) */
		InhibitedCoin09 = 136,
		/*Inhibited coin ( Type 10 ) */
		InhibitedCoin10 = 137,
		/*Inhibited coin ( Type 11 ) */
		InhibitedCoin11 = 138,
		/*Inhibited coin ( Type 12 ) */
		InhibitedCoin12 = 139,
		/*Inhibited coin ( Type 13 ) */
		InhibitedCoin13 = 140,
		/*Inhibited coin ( Type 14 ) */
		InhibitedCoin14 = 141,
		/*Inhibited coin ( Type 15 ) */
		InhibitedCoin15 = 142,
		/*Inhibited coin ( Type 16 ) */
		InhibitedCoin16 = 143,
		/*Inhibited coin ( Type 17 ) */
		InhibitedCoin17 = 144,
		/*Inhibited coin ( Type 18 ) */
		InhibitedCoin18 = 145,
		/*Inhibited coin ( Type 19 ) */
		InhibitedCoin19 = 146,
		/*Inhibited coin ( Type 20 ) */
		InhibitedCoin20 = 147,
		/*Inhibited coin ( Type 21 ) */
		InhibitedCoin21 = 148,
		/*Inhibited coin ( Type 22 ) */
		InhibitedCoin22 = 149,
		/*Inhibited coin ( Type 23 ) */
		InhibitedCoin23 = 150,
		/*Inhibited coin ( Type 24 ) */
		InhibitedCoin24 = 151,
		/*Inhibited coin ( Type 25 ) */
		InhibitedCoin25 = 152,
		/*Inhibited coin ( Type 26 ) */
		InhibitedCoin26 = 153,
		/*Inhibited coin ( Type 27 ) */
		InhibitedCoin27 = 154,
		/*Inhibited coin ( Type 28 ) */
		InhibitedCoin28 = 155,
		/*Inhibited coin ( Type 29 ) */
		InhibitedCoin29 = 156,
		/*Inhibited coin ( Type 30 ) */
		InhibitedCoin30 = 157,
		/*Inhibited coin ( Type 31 ) */
		InhibitedCoin31 = 158,
		/*Inhibited coin ( Type 32 ) */
		InhibitedCoin32 = 159,
		/* Data block request ( note_α ) */
		DataBlockRequest = 253,
		/*Coin return mechanism activated ( flight deck open ) */
		CoinReturnMechanismActivated = 254,
		/*Unspecified alarm code */
		UnspecifiedAlarmCode = 255,
	}
}