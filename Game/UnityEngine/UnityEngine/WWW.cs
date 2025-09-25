using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	public class WWW : IDisposable
	{
		private IntPtr wwwWrapper;

		public extern string data
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern byte[] bytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int size
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string error
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Texture2D texture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AudioClip audioClip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern MovieTexture movie
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isDone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float progress
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float uploadProgress
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AudioClip oggVorbis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string url
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AssetBundle assetBundle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ThreadPriority threadPriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public WWW(string url)
			: this(url, null, null)
		{
		}

		public WWW(string url, WWWForm form)
		{
			Hashtable headers = form.headers;
			string[] array = null;
			if (headers != null)
			{
				array = new string[headers.Count * 2];
				int num = 0;
				foreach (object item in headers)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)item;
					array[num++] = dictionaryEntry.Key.ToString();
					array[num++] = dictionaryEntry.Value.ToString();
				}
			}
			InitWWW(url, form.data, array);
		}

		public WWW(string url, byte[] postData)
			: this(url, postData, null)
		{
		}

		public WWW(string url, byte[] postData, Hashtable headers)
		{
			string[] array = null;
			if (headers != null)
			{
				array = new string[headers.Count * 2];
				int num = 0;
				foreach (object header in headers)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)header;
					array[num++] = dictionaryEntry.Key.ToString();
					array[num++] = dictionaryEntry.Value.ToString();
				}
			}
			InitWWW(url, postData, array);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern WWW(string url, int version);

		public virtual void Dispose()
		{
			DestroyWWW(true);
		}

		~WWW()
		{
			DestroyWWW(false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DestroyWWW(bool cancel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitWWW(string url, byte[] postData, string[] iHeaders);

		public static string EscapeURL(string s)
		{
			Encoding uTF = Encoding.UTF8;
			return EscapeURL(s, uTF);
		}

		public static string EscapeURL(string s, Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s == "")
			{
				return "";
			}
			return WWWTranscoder.URLEncode(s, e);
		}

		public static string UnEscapeURL(string s)
		{
			Encoding uTF = Encoding.UTF8;
			return UnEscapeURL(s, uTF);
		}

		public static string UnEscapeURL(string s, Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
			{
				return s;
			}
			return WWWTranscoder.URLDecode(s, e);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LoadImageIntoTexture(Texture2D tex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("All blocking WWW functions have been deprecated, please use one of the asynchronous functions instead.", true)]
		public static extern string GetURL(string url);

		[Obsolete("All blocking WWW functions have been deprecated, please use one of the asynchronous functions instead.", true)]
		public static Texture2D GetTextureFromURL(string url)
		{
			return new WWW(url).texture;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LoadUnityWeb();

		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			return new WWW(url, version);
		}
	}
}
