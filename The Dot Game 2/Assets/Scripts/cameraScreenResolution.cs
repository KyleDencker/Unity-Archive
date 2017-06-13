using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScreenResolution : MonoBehaviour {

	void Start() {
		float targetaspect = 750.0f / 1337.0f;

		float windowaspect = (float)Screen.width / (float)Screen.height;

		Camera.main.projectionMatrix = Matrix4x4.Scale(new Vector3(windowaspect / targetaspect, 1.0f, 1.0f)) * Camera.main.projectionMatrix;

	}

}
