using UnityEngine;
using System.Collections;

public class Football : Hazard {

	Vector2 MoveDirection;

	public float Speed = 10.0f;

	bool HasPlayerBooted;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		Vector2 StartingPosition = Camera.main.ScreenToWorldPoint(PickStartPosition());
		transform.position = StartingPosition;

		float RandomAngle = Random.Range(-45.0f, 45.0f);
		var Rotator = Quaternion.AngleAxis(RandomAngle, Vector3.forward);
		MoveDirection = Rotator * ((-StartingPosition).normalized);
		Debug.DrawLine(StartingPosition, StartingPosition + 5.0f * MoveDirection, Color.white, 1.0f);

		HasPlayerBooted = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		rbody.velocity = MoveDirection * Speed;
	}

	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		base.OnTriggerEnter2D(collider);

		if(collider.tag != Utility.CandleTag && FullyEnteredGarden)
		{
			if(collider.tag == Utility.PlayerTag || !HasPlayerBooted)
			{
				Vector2 NormalOfCollision = ((Vector2)(transform.position - collider.transform.position)).normalized;
				MoveDirection = Vector2.Reflect(MoveDirection, NormalOfCollision);
			}
		}	
	}

	protected override void OnPlayerCollision(Collider2D Player)
	{
		base.OnPlayerCollision(Player);
		HasPlayerBooted = true;
	}
}
