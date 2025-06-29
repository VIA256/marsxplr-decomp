using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	public class ZlibStream : Stream
	{
		internal ZlibBaseStream _baseStream;

		private bool _disposed;

		public virtual FlushType FlushMode
		{
			get
			{
				return _baseStream._flushMode;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				_baseStream._flushMode = value;
			}
		}

		public int BufferSize
		{
			get
			{
				return _baseStream._bufferSize;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				if (_baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 128)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer.", value));
				}
				_baseStream._bufferSize = value;
			}
		}

		public virtual long TotalIn
		{
			get
			{
				return _baseStream._z.TotalBytesIn;
			}
		}

		public virtual long TotalOut
		{
			get
			{
				return _baseStream._z.TotalBytesOut;
			}
		}

		public override bool CanRead
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return _baseStream._stream.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return _baseStream._stream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return _baseStream._z.TotalBytesOut;
				}
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return _baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			_baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!_disposed)
				{
					if (disposing && _baseStream != null)
					{
						_baseStream.Close();
					}
					_disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public override void Flush()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			_baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return _baseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			_baseStream.Write(buffer, offset, count);
		}

		public static byte[] CompressString(string s)
		{
			Encoding uTF = Encoding.UTF8;
			byte[] bytes = uTF.GetBytes(s);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression))
				{
					stream.Write(bytes, 0, bytes.Length);
				}
				return memoryStream.ToArray();
			}
		}

		public static byte[] CompressBuffer(byte[] b)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression))
				{
					stream.Write(b, 0, b.Length);
				}
				return memoryStream.ToArray();
			}
		}

		public static string UncompressString(byte[] compressed)
		{
			byte[] array = new byte[1024];
			Encoding uTF = Encoding.UTF8;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (MemoryStream stream = new MemoryStream(compressed))
				{
					using (Stream stream2 = new ZlibStream(stream, CompressionMode.Decompress))
					{
						int count;
						while ((count = stream2.Read(array, 0, array.Length)) != 0)
						{
							memoryStream.Write(array, 0, count);
						}
					}
					memoryStream.Seek(0L, SeekOrigin.Begin);
					StreamReader streamReader = new StreamReader(memoryStream, uTF);
					return streamReader.ReadToEnd();
				}
			}
		}

		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array = new byte[1024];
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (MemoryStream stream = new MemoryStream(compressed))
				{
					using (Stream stream2 = new ZlibStream(stream, CompressionMode.Decompress))
					{
						int count;
						while ((count = stream2.Read(array, 0, array.Length)) != 0)
						{
							memoryStream.Write(array, 0, count);
						}
					}
					return memoryStream.ToArray();
				}
			}
		}
	}
}
