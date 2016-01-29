using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour {

	Vector2 Target;

	public float Speed = 10.0f;
	public float RampUpTime = 0.5f;
	public AnimationCurve AccelerationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
	float TimeElapsed;
    Rigidbody2D rbody;

	public float ArrivalDistance = 1.0f;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
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

	Vector2 PickStartPosition()
	{
		int Side =Random.Range(0, 4);
		int xPos = Random.Range(0, Screen.width);	
		int yPos = Random.Range(0, Screen.height);
		switch(Side)
		{
			case 0:
				return new Vector2(xPos, 0);

			case 1:
				return new Vector2(xPos, Screen.height);

			case 2:
				return new Vector2(0, yPos);

			case 3:
				return new Vector2(Screen.width, yPos);

		}
		
		return Vector2.zero;
	}

	Vector2 PickTarget()
	{
		int xPos = Random.Range(0, Screen.width);
		int yPos = Random.Range(0, Screen.height);

		return new Vector2(xPos, yPos);
	}
}
