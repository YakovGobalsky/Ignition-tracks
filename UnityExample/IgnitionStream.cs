using System.IO;
using UnityEngine;

namespace IgnitionTrack {
	public class IgnitionStream: MemoryStream {
		public IgnitionStream(byte[] buffer) : base(buffer) {
		}

		public byte ReadInt8() => (byte)ReadByte();
		public int ReadInt16() => ReadInt8() + (ReadInt8() << 8);
		public int ReadInt32() => ReadInt16() + (ReadInt16() << 16);

        public Vector2 ReadVector2()
        {
            return new Vector2(ReadInt32(), ReadInt32());
        }

        public Vector3 ReadVector3() {
            return new Vector3(ReadInt32(), -ReadInt32(), -ReadInt32());
        }


    }
}