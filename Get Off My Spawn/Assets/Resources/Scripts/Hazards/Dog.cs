using UnityEngine;
using System.Collections;

public class Dog : Hazard {

	Chase_Player c;

	// Use this for initialization
	protected override void Start () {
		base.Start();	
		c = GetComponent<Chase_Player> ();
		//if (Random.Range (0, 5)> 0) {
			c.target = GameObject.FindGameObjectWithTag (Utility.PlayerTag).transform;
	//	} else {
	//		c.target = GameObject.FindObjectOfType<Hazard> ().transform;
//		}
		transform.position = PickStartPosition ();

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	
	}
}
