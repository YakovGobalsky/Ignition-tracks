using System.IO;

namespace IgnitionTrack {
	public class IgnitionStream: MemoryStream {
		public IgnitionStream(byte[] buffer) : base(buffer) {
		}

		public byte ReadInt8() => (byte)ReadByte();
		public int ReadInt16() => ReadInt8() + (ReadInt8() << 8);
		public int ReadInt32() => ReadInt16() + (ReadInt16() << 16);
	}
}