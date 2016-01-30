using UnityEngine;
using System.Collections;
using System.Linq;

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
		Candle TargetCandle = PickTarget();
		if(TargetCandle != null)
		{
			Target = GetTargetPosition(TargetCandle);
			Vector2 StartingPosition = PickStartPosition();
			transform.position = StartingPosition;
		}
		else
		{
			// No candles to target - don't lob a water balloon
			Destroy(gameObject);
		}

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

	Candle PickTarget()
	{
		Candle[] PossibleCandles = GameObject.FindObjectsOfType<Candle>()
										.Where(Candle => Candle.GetIsCandleLit())
										.ToArray();

		if(PossibleCandles.Length > 0)
		{
			Candle SelectedCandle = PossibleCandles[Random.Range(0, PossibleCandles.Length)];
	
			return SelectedCandle;
			
		}
		else
		{
			return null;
		}
	}

	Vector2 GetTargetPosition(Candle Target)
	{
		Vector2 MissVector = Random.insideUnitCircle;
		return ((Vector2)Target.transform.position) + (MissVector * Inaccuracy);
	}

	protected override void OnTriggerEnter2D(Collider2D collid)
	{
		base.OnTriggerEnter2D(collid);
		if(FullyEnteredGarden)
		{
			StartCoroutine(ExplodeWaterBalloon());
		}
        // Did we just hit a candle?
        if (collid.tag == "Candle")
        {
            collid.GetComponent<Candle>().SetCandleLit(false);
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
