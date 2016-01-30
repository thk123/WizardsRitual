using UnityEngine;
using System.Collections;

public class Chase_Player_SpriteSheet : MonoBehaviour {


	public Transform target;//set target from inspector instead of looking in Update
	public float speed = 3f;


	void Start () {

	}

	void Update(){

		Vector3 dist = (target.position - transform.position);
		float angle = 90- (Mathf.Atan2 (dist.x, dist.y) * Mathf.Rad2Deg);
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90*Time.deltaTime);

		//rotate to look at the player
		//move towards the player
		if (dist.magnitude>1.5){//move if distance from target is greater than 1
			Vector2 translation = (Vector2) (q * new Vector3(speed*Time.deltaTime, 0, 0));

			transform.Translate(translation );
		}
		if (angle > 180 && transform.localScale.x < 0) {
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}
		if (angle < 180 && transform.localScale.x > 0) {
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}


	}

}
