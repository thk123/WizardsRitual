using UnityEngine;
using System.Collections;

public class WaterBalloon : Hazard {

	Vector2 Target;

	public float Speed = 10.0f;
	public float RampUpTime = 0.5f;

	public float Inaccuracy = 1.0f;

	public AnimationCurve AccelerationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
	float TimeElapsed;
    

	public float ArrivalDistance = 1.0f;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		Target = PickTarget();
		Vector2 StartingPosition = Camera.main.ScreenToWorldPoint(PickStartPosition());
		transform.position = StartingPosition;

		TimeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
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
			StartCoroutine(ExplodeWaterBalloon());
		}
	}

	Vector2 PickTarget()
	{
		Candle[] PossibleCandles = GameObject.FindObjectsOfType<Candle>();

		Candle SelectedCandle = PossibleCandles[Random.Range(0, PossibleCandles.Length)];

		Vector2 MissVector = Random.insideUnitCircle;

		return ((Vector2)SelectedCandle.transform.position) + (MissVector * Inaccuracy);
	}

	protected override void OnTriggerEnter2D(Collider2D collid)
	{
		base.OnTriggerEnter2D(collid);
		if(FullyEnteredGarden)
		{
			StartCoroutine(ExplodeWaterBalloon());
		}
	}

	private IEnumerator ExplodeWaterBalloon()
	{
		Speed = 0.0f;
		rbody.velocity = Vector2.zero;

		Animator balloonAnimator = GetComponent<Animator>();
		if(balloonAnimator != null)
		{
			balloonAnimator.SetBool("Exploded", true);
		}

		AudioSource AudioEffect = GetComponent<AudioSource>();
		while(AudioEffect.isPlaying)
		{
			yield return null;
		}

		GameObject.Destroy(gameObject);
	}
}
