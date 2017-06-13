using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour {

	private Transform player;
	public float speed;
	public float angle;
	public float last = 0;
	public float rotate = 1;

	public GameObject attached;


	// Use this for initialization
	void Start () {
		Input.multiTouchEnabled = true;
		player = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (attached == null) {
			player.position += new Vector3 (Mathf.Cos (DegreeToRadian (angle)) * speed, Mathf.Sin (DegreeToRadian (angle)) * speed);
		} else {
			angle += 5 * rotate;
			angle %= 360;
			Vector3 dotCenter = attached.transform.position;
			Vector3 dotScale = attached.transform.localScale;
	
			Vector3 newPosition = new Vector3 (	dotCenter.x + (dotScale.x/2)*Mathf.Cos (DegreeToRadian(angle)), 
												dotCenter.y + (dotScale.y/2)*Mathf.Sin (DegreeToRadian(angle)), 
												0);
			
			player.position = newPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Dots") {
			attached = other.gameObject;

			float deltaX = player.position.x - other.gameObject.transform.position.x;
			float deltaY = player.position.y - other.gameObject.transform.position.y;

			float newAngle = (180.0f / Mathf.PI * Mathf.Atan2 (deltaY, deltaX)+720)%360;
			float oldAngle = (this.angle + 720)%360;

			if (newAngle > 270 && oldAngle < 90) {
				oldAngle += 360;
			}
			rotate = 1;

			if (newAngle < oldAngle) {
				rotate = -1;
			}


			this.angle = newAngle;


			float current = other.gameObject.GetComponent<DotValues> ().position;
			float difference = current - last;
			last = current;
			if (difference > 0)
				PlayerScore.playerScore += Mathf.Pow (2, difference-1);
			else if (difference < 0) 
				PlayerScore.playerScore += 5 * Mathf.Pow (2, Mathf.Abs (difference+1)); 

		}
	}

	void Update () {
		// Desktop Control
		if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) {
			attached = null;
			//angle += 90;
		}

		// Mobile Control
		if (Input.touchCount > 0) {
			attached = null;
		}

		if (Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene ("mainGame");
		}

		if (Mathf.Abs (player.position.x) > 10 || Mathf.Abs (player.position.y) > 15) {
			PlayerScore.playerScore = 0;
			SceneManager.LoadScene ("mainGame");
		}

	}

	private float DegreeToRadian(float angle)
	{
		return angle * ((float)Mathf.PI / 180.0f);
	}
}
