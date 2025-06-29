namespace UnityEngine
{
	public enum NetworkConnectionError
	{
		NoError = 0,
		RSAPublicKeyMismatch = 20,
		InvalidPassword = 22,
		ConnectionFailed = 14,
		TooManyConnectedPlayers = 17,
		ConnectionBanned = 21,
		AlreadyConnectedToAnotherServer = -1,
		CreateSocketOrThreadFailure = -2,
		IncorrectParameters = -3,
		EmptyConnectTarget = -4,
		InternalDirectConnectFailed = -5,
		NATTargetNotConnected = 61,
		NATTargetConnectionLost = 62
	}
}
