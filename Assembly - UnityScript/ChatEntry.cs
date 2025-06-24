using System;

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
