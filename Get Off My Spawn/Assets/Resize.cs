using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Play() {
		anim.SetTrigger ("zoom_in");
	}

}
