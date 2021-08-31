using UnityEngine;

namespace IgnitionTrack {
	public class IgnitionPalette {
		/*
		 * //file .COL
		 * struct Colours {
		 *	int32 file_length;
		 *	int32 unknown;
		 *	uint8 colours[256][3]; //colour #0 = transparent!
		 * }
		 */

		private readonly Color32[] colours = new Color32[256];
		public Color32 GetColour(int index) => colours[index];

		public IgnitionPalette(IgnitionStream stream) {
			_ = stream.ReadInt32(); //file length
			_ = stream.ReadInt32(); //unknown

			for (int i = 0; i < 256; i++) {
				colours[i] = new Color32(stream.ReadInt8(), stream.ReadInt8(), stream.ReadInt8(), 0xFF);
			}
			colours[0].a = 0; //colour #0 = transparent!
		}
	}
}