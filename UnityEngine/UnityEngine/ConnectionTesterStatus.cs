namespace UnityEngine
{
	public enum ConnectionTesterStatus
	{
		Error = -2,
		Undetermined,
		PrivateIPNoNATPunchthrough,
		PrivateIPHasNATPunchThrough,
		PublicIPIsConnectable,
		PublicIPPortBlocked,
		PublicIPNoServerStarted
	}
}
