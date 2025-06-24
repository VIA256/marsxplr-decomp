using System;

[Serializable]
public class GameWorldDesc
{
	public string name;

	public string url;

	public bool featured;

	private bool _0024initialized__GameWorldDesc_0024;

	public GameWorldDesc()
	{
		if (!_0024initialized__GameWorldDesc_0024)
		{
			name = string.Empty;
			url = string.Empty;
			_0024initialized__GameWorldDesc_0024 = true;
		}
	}

	public GameWorldDesc(string n, string u, bool feat)
	{
		if (!_0024initialized__GameWorldDesc_0024)
		{
			name = string.Empty;
			url = string.Empty;
			_0024initialized__GameWorldDesc_0024 = true;
		}
		name = n;
		url = u;
		featured = feat;
	}
}
