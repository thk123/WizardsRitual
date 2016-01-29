using UnityEngine;
using System.Collections;

public class PIvec2 {

	// A PI controller for vector 2D quantities

	private float P, I, D;
	private float I_max;
	private Vector2 I_curr;
	private Vector2 X_set;
    private Vector2 X_last;

    public PIvec2(float P, float I, float D, float I_max) {
		this.P = P;
		this.I = I;
        this.D = D;
		this.I_max = I_max;

		I_curr = Vector2.zero;
		X_set = Vector2.zero;
        X_last = Vector2.zero;
	}

	public void SetPoint(Vector2 X_set) {
		this.X_set = X_set;
	}

	public Vector2 Resp(Vector2 X) {
		Vector2 delta = (X_set - X);
		I_curr += delta;
		if (I_curr.magnitude > I_max) {
			I_curr = I_curr.normalized*I_max;
		}
        delta = P * delta + I * I_curr + D * (X - X_last);
        X_last = X;
		return delta;
	}
	 
}
