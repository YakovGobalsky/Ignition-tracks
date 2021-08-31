using UnityEngine;

namespace IgnitionTrack {
	public static class IgnitionTexture {
		/*
		 * //file .TEX
		 * struct Textures {
		 *	struct Texture {
		 *		uint8 colour_indexes[256][256]
		 *	}
		 *  Texture textures[16]; //1024bytes total (except JAPAN.TEX)
		 * }
		 */

		public static Texture2D LoadTexture(IgnitionStream stream, IgnitionPalette palette) {
			Texture2D texture = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);

			for (int tx = 0; tx < 4; tx++) {
				for (int ty = 0; ty < 4; ty++) {
					for (int x = 0; x < 256; x++) {
						for (int y = 0; y < 256; y++) {
							byte colourIndex = stream.ReadInt8();
							texture.SetPixel(x + 256 * tx, y + 256 * ty, palette.GetColour(colourIndex));
						}
					}
				}
			}

			texture.Apply();
			return texture;
		}
	}
}