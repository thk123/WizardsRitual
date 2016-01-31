using UnityEngine;
using System.Collections;

public class Chase_Player : MonoBehaviour {


	public Transform target;//set target from inspector instead of looking in Update
	public float speed = 3f;
	bool jumping = false;
	bool prepping = false;
	public float jumpDistance = 1.5f;
	public float prepProgress = 0;

	Vector3 StartingScale;

	void Start () {
		StartingScale = transform.localScale;
	}

	void Update(){
		if (jumping) {
			if (prepping) {
				prep ();
			} else {
				fly ();
			}
		} else {
			chase ();
		}
			
	}

	public bool IsJumping()
	{
		return jumping;
	}

	void chase(){
		Vector3 dist = (target.position - transform.position);
		float DirectionMultiplier;
		Quaternion q = UpdateRotation(dist, out DirectionMultiplier);
		transform.rotation = q;
	

		//rotate to look at the player
		//move towards the player
		if (dist.magnitude > jumpDistance) {//move if distance from target is greater than 1
			transform.Translate (new Vector2 (speed * Time.deltaTime * DirectionMultiplier, 0));

		} else {
			jumping = true;
			prepping = true;
		}
	}

	void prep() {
		prepProgress += Time.deltaTime;
		if (prepProgress > 1) {
			prepping = false;
			GetComponent<AudioSource>().Play();
		} else {
			Vector3 dist = (target.position - transform.position);
			float DirectionMultiplier;
			Quaternion q = UpdateRotation(dist, out DirectionMultiplier);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime);
			transform.Translate (new Vector2 (-0.1f* speed * Time.deltaTime * DirectionMultiplier, 0));

			FlyDirection = DirectionMultiplier;
		}
		
	}

	float FlyDirection;

	void fly() {
		Vector3 dist = (target.position - transform.position);
		/*float DirectionMultiplier;
		Quaternion q = UpdateRotation(dist, out DirectionMultiplier);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime*0.3f);*/
		transform.Translate (new Vector2 (speed*4 * Time.deltaTime * FlyDirection, 0));
	}

	public Quaternion UpdateRotation(Vector2 dist, out float ReverseDirection)
	{
		if(dist.x > 0.0f)
		{
			transform.localScale = StartingScale;
			float angle = 90 - (Mathf.Atan2 (dist.x, dist.y) * Mathf.Rad2Deg);
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			
			ReverseDirection = 1.0f;

			return q;
		}
		else
		{
			// Reverse the scale
			transform.localScale = Vector3.Scale(StartingScale, new Vector3(-1.0f, 1.0f, 1.0f));
			float angle = 90 - (Mathf.Atan2 (-dist.x, -dist.y) * Mathf.Rad2Deg);
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			
			ReverseDirection = -1.0f;

			return q;
		}
	}

}
