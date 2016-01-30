using UnityEngine;
using System.Collections;

public class WaterBalloon : Hazard {

	Vector2 Target;

	public float Speed = 10.0f;
	public float RampUpTime = 0.5f;
	public AnimationCurve AccelerationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
	float TimeElapsed;
    

	public float ArrivalDistance = 1.0f;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		Target = Camera.main.ScreenToWorldPoint(PickTarget());
		Vector2 StartingPosition = Camera.main.ScreenToWorldPoint(PickStartPosition());
		transform.position = StartingPosition;

		TimeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 DistanceToGoVector = Target - ((Vector2)transform.position);
		float DistanceToGo = DistanceToGoVector.sqrMagnitude;

		if(DistanceToGo > ArrivalDistance * ArrivalDistance)
		{
			TimeElapsed += Time.deltaTime;
			
			float Velocity = Speed * AccelerationCurve.Evaluate(TimeElapsed / RampUpTime);
            rbody.velocity = DistanceToGoVector.normalized * Velocity;
		}
		else
		{
			GameObject.Destroy(gameObject);
		}
	}

	Vector2 PickTarget()
	{
		int xPos = Random.Range(0, Screen.width);
		int yPos = Random.Range(0, Screen.height);

		return new Vector2(xPos, yPos);
	}

	protected override void OnTriggerEnter2D(Collider2D collid)
	{
		base.OnTriggerEnter2D(collid);

		StartCoroutine(ExplodeWaterBalloon());
	}

	private IEnumerator ExplodeWaterBalloon()
	{
		Speed = 0.0f;
		AudioSource AudioEffect = GetComponent<AudioSource>();
		while(AudioEffect.isPlaying)
		{
			yield return null;
		}

		GameObject.Destroy(gameObject);
	}
}
