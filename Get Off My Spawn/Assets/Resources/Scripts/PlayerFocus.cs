using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFocus : MonoBehaviour {

	public float velocityThreshold = 0.1f;
	public float MaxVelocity = 4.0f;
	public AnimationCurve DecayRate; 

	public float RecoveryPerSecond = 0.1f;

	public float CurrentFocus
	{
		get;
		private set;
	}

	// Use this for initialization
	void Start () {
		CurrentFocus = 1.0f;
	}
	
	// Update is called once per frames
	void Update () {
		float CurrentVelocity = GetComponent<Rigidbody2D>().velocity.magnitude;

		float ShiftedVeloicty = Mathf.Max(CurrentVelocity - velocityThreshold, 0.0f);

		float DecayPerSecond = DecayRate.Evaluate(ShiftedVeloicty /(MaxVelocity - velocityThreshold));

		CurrentFocus += (RecoveryPerSecond - DecayPerSecond) * Time.deltaTime;
		CurrentFocus = Mathf.Clamp(CurrentFocus, 0.0f, 1.0f);
	}
}
