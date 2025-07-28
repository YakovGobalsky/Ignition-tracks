using UnityEngine;

namespace IgnitionTrack {
    public static class IgnitionMeshes {
        /*
		 * //file .MSH
		 * struct Mesh {
		 *	struct Poly {
		 *		int32 unknown1; 
		 *		int32 v[3];			//vertex indexes
		 *		int32 uv[3][2]; //uvs for each vertex
		 *		int16 unknown2; //unknown1/2 - effects?? (19:78 => clouds; 17:303 => rolling balls)
		 *		uint16 texture; //byte+byte?
		 *	}
		 *	
		 *	int32 vCount;
		 *	int32[vCount][3] vertices; //x,y,z
		 *	int32 pCount;
		 *	Poly[pCount] polys;
		 * }
		 *
		 * //file .PLC
		 * struct Places {
		 *	int32 meshesCount;
		 *	int32 unknown[2];
		 *	int32 position[3]; //x,-y(??),z
		 * }
		 * 
		 */

        //private readonly List<GameObject> meshes = new List<GameObject>();
        public struct Poly {
            public int[] indexes;
            public Vector2[] uvs;
            public int texture;

            public Poly(IgnitionStream stream) {
                _ = stream.ReadInt32();
                indexes = new int[3] { stream.ReadInt32(), stream.ReadInt32(), stream.ReadInt32() };
                uvs = new Vector2[3] {
                    stream.ReadVector2(),
                    stream.ReadVector2(),
                    stream.ReadVector2(),
                };
                _ = stream.ReadInt16();
                texture = stream.ReadInt16();
            }

            public Vector2 GetUV(Vector2 pos) => new Vector2(pos.y / 65535f + texture / 4, pos.x / 65535f + texture % 4) * 0.25f;

            public Vector2 GetUV(int index) => GetUV(uvs[index]);
        }

        public static void CreateMeshes(IgnitionStream streamMeshes, IgnitionStream streamPlaces, Material material, Transform parent) {
            int cnt = streamPlaces.ReadInt32(); //meshes count

            for (int meshIndex = 0; meshIndex < cnt; meshIndex++) {
                var mesh = LoadMesh(streamMeshes, meshIndex);

                _ = streamPlaces.ReadInt32(); //unknown
                _ = streamPlaces.ReadInt32(); //unknown

                Vector3 position = streamPlaces.ReadVector3();
                position.y = -position.y;

                mesh.transform.parent = parent;
                mesh.GetComponent<MeshRenderer>().material = material;
                mesh.transform.localScale = Vector3.one;
                mesh.transform.localPosition = position;
                //meshes.Add(mesh);
            }
        }

        private static GameObject LoadMesh(IgnitionStream stream, int meshIndex) {
            int verticesCount = stream.ReadInt32();
            int polyCount = stream.ReadInt32();

            Vector3[] v = new Vector3[verticesCount];
            for (int i = 0; i < verticesCount; i++) {
                v[i] = stream.ReadVector3();
            }

            Poly[] p = new Poly[polyCount];
            for (int i = 0; i < polyCount; i++) {
                p[i] = new Poly(stream);
            }

            var obj = new GameObject(string.Format("obj{0}", meshIndex), typeof(MeshFilter), typeof(MeshRenderer));
            var mf = obj.GetComponent<MeshFilter>();

            var mesh = new Mesh();

            var mv = new Vector3[polyCount * 3];
            var muv = new Vector2[polyCount * 3];
            var mt = new int[polyCount * 3];

            for (int i = 0; i < polyCount; i++) {
                mv[i * 3 + 0] = v[p[i].indexes[0]];
                mv[i * 3 + 1] = v[p[i].indexes[1]];
                mv[i * 3 + 2] = v[p[i].indexes[2]];

                muv[i * 3 + 0] = p[i].GetUV(0);
                muv[i * 3 + 1] = p[i].GetUV(1);
                muv[i * 3 + 2] = p[i].GetUV(2);

                mt[i * 3 + 0] = i * 3 + 0;
                mt[i * 3 + 1] = i * 3 + 2;
                mt[i * 3 + 2] = i * 3 + 1;
            }

            mesh.vertices = mv;
            mesh.triangles = mt;
            mesh.uv = muv;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            mf.mesh = mesh;

            return obj;
        }

    }

}