using UnityEngine;

namespace IgnitionTrack {
	public struct Vec2 {
		public int x, y;
		public Vec2(IgnitionStream stream) {
			x = stream.ReadInt32();
			y = stream.ReadInt32();
		}
	}

	public struct Vec3 {
		public int x, y, z;

		public Vec3(IgnitionStream stream) {
			x = stream.ReadInt32();
			y = stream.ReadInt32();
			z = stream.ReadInt32();
		}

		public Vector3 GetVector3() => new Vector3(x, -y, -z); //to unitys coordinate system
	}

	public struct Poly {
		public Vec3 vertices;
		public Vec2[] uvs;
		public int texture;

		public Poly(IgnitionStream stream) {
			_ = stream.ReadInt32();
			vertices = new Vec3(stream);
			uvs = new Vec2[3] {
					new Vec2(stream),
					new Vec2(stream),
					new Vec2(stream),
				};
			_ = stream.ReadInt16();
			texture = stream.ReadInt16();
		}

		public Vector2 GetUV(Vec2 pos) => new Vector2(pos.y / 65535f + texture / 4, pos.x / 65535f + texture % 4) * 0.25f;

		public Vector2 GetUV(int index) => GetUV(uvs[index]);
	}

}