using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour {

	private Transform dotHolder;

	public Material[] materials;

	public Transform spawn;
	public float spawnRate;
	public float nextSpawn;
	public bool mustBeDot;
	public float gameSpeed;
	Renderer rend;
	private float lastPosition = 4;

	// Use this for initialization
	void Start () {
		dotHolder = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Transform dot in dotHolder) {
			dot.position += Vector3.down*gameSpeed;

			if (dot.position.y < -15) {
				Destroy (dot.gameObject);
			}
		}

		if (Time.time >= nextSpawn) {
			nextSpawn = Time.time + spawnRate;
			Transform temp = Instantiate (spawn);
			temp.position = new Vector3 (Random.Range (-4, 4), 15f, 0f);
			temp.localScale *= Random.Range(2.5f, 4f);
			temp.SetParent (dotHolder);
			temp.gameObject.GetComponent<MeshRenderer>().material = materials [(int)Random.Range (0, 4)];
			temp.gameObject.GetComponent<DotValues> ().position = lastPosition;
			lastPosition++;

		}





	}
}
