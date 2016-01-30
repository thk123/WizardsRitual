﻿using UnityEngine;
using System.Collections;

public class Chase_Player : MonoBehaviour {


	public Transform target;//set target from inspector instead of looking in Update
	public float speed = 3f;
	bool jumping = false;

	void Start () {

	}

	void Update(){
		if (jumping) {
			jump ();
		} else {
			chase ();
		}
			
	}

	void chase(){
		Vector3 dist = (target.position - transform.position);
		float angle = 90 - (Mathf.Atan2 (dist.x, dist.y) * Mathf.Rad2Deg);
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = q;

	

		//rotate to look at the player
		//move towards the player
		if (dist.magnitude > 1.5) {//move if distance from target is greater than 1
			transform.Translate (new Vector2 (speed * Time.deltaTime, 0));

		} else {
			jumping = true;
		}
	}

	void jump() {
		transform.Translate (new Vector2 (speed*4 * Time.deltaTime, 0));
	}

}
