using System;

[Serializable]
public enum chatOrigins
{
    Local,
    Remote,
    Server
}

[Serializable]
public class ChatEntry
{
	public string text;

	public chatOrigins origin;

	public ChatEntry()
    {
		text = string.Empty;
	}
}
