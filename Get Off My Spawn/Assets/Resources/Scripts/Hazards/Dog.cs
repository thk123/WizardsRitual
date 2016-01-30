using UnityEngine;
using System.Collections;

public class Dog : Hazard {

	Chase_Player c;

	// Use this for initialization
	protected override void Start () {
		base.Start();	
		c = GetComponent<Chase_Player> ();
		c.target = GameObject.FindGameObjectWithTag(Utility.PlayerTag).transform;
		transform.position = PickStartPosition ();

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	
	}
}
