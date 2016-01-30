using UnityEngine;
using System.Collections;

public class Chase_Player_SpriteSheet : MonoBehaviour {


	public Transform target;//set target from inspector instead of looking in Update
	public float speed = 3f;
	public float angle = 0;

	Animator anim;


	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update(){

		Vector3 dist = (target.position - transform.position);
		angle = 90- (Mathf.Atan2 (dist.x, dist.y) * Mathf.Rad2Deg);
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90*Time.deltaTime);

		//rotate to look at the player
		//move towards the player
		Vector2 translation = new Vector2(0, 0);
		if (dist.magnitude>1.5){//move if distance from target is greater than 1
			translation = (Vector2) (q * new Vector3(speed*Time.deltaTime, 0, 0));

			transform.Translate(translation );
		}

		if (Mathf.Abs (translation.y) > Mathf.Abs (translation.x)) {
			if (translation.y < 0) {
				anim.SetInteger ("Direction", 2);
			} else {
				anim.SetInteger ("Direction", 1);
			}
		}
			
		else if (Mathf.Abs (translation.y) < Mathf.Abs (translation.x)){
			anim.SetInteger ("Direction", 0);
		}

	}

}
