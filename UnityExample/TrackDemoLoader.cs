using UnityEngine;

namespace IgnitionTrack {
	public class TrackDemoLoader: MonoBehaviour {
		[SerializeField] private TextAsset fileMesh;    //CANADA.MSH
		[SerializeField] private TextAsset filePlaces;  //CANADA.PLC
		[SerializeField] private TextAsset filePalette; //CANADA.COL
		[SerializeField] private TextAsset fileTexture; //CANADA.TEX

		[SerializeField] private Shader shader; //w transparancy. eg "Unlit/Transparent Cutout" (or replace with material)

		private void Start() {
			using var streamPalette = new IgnitionStream(filePalette.bytes);
			IgnitionPalette palette = new IgnitionPalette(streamPalette);

			using var streamTexture = new IgnitionStream(fileTexture.bytes);
			Material material = new Material(shader) {
				mainTexture = IgnitionTexture.LoadTexture(streamTexture, palette)
			};
			//material.mainTexture = texture.texture;

			using var streamMeshes = new IgnitionStream(fileMesh.bytes);
			using var streamPlaces = new IgnitionStream(filePlaces.bytes);
			IgnitionMeshes.CreateMeshes(streamMeshes, streamPlaces, material, transform);
		}
	}

}