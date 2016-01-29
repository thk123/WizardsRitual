using UnityEngine;
using System.Collections;

public class PhysicsPlayerControl : MonoBehaviour {

    // Use this for initialization
    public float P, I, D, Imax;

    Animator anim;
    Rigidbody2D rbody;
    PIvec2 pid_control;

	void Awake () {
        pid_control = new PIvec2(P, I, D, Imax);
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Get the mouse position, that's our set point
        pid_control.SetPoint((Vector2)MouseCursorScript.sngl.transform.position);
        // Now get the force
        rbody.AddForce(pid_control.Resp((Vector2)transform.position));
        anim.speed = rbody.velocity.magnitude;
	}
}
