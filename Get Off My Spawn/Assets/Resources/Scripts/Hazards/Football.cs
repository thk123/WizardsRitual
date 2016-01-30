using UnityEngine;
using System.Collections;

public class Football : Hazard {

	Vector2 MoveDirection;

	public float Speed = 10.0f;

	// Use this for initialization
	void Start () {
		Vector2 StartingPosition = Camera.main.ScreenToWorldPoint(PickStartPosition());
		transform.position = StartingPosition;

		float RandomAngle = Random.Range(-45.0f, 45.0f);
		var Rotator = Quaternion.AngleAxis(RandomAngle, Vector3.forward);
		MoveDirection = Rotator * (-StartingPosition).normalized;
	}
	
	// Update is called once per frame
	void Update () {
		rbody.velocity = MoveDirection * Speed;
	}

	protected override void OnTriggerEnter2D(Collider2D collid)
	{
		base.OnTriggerEnter2D(collid);

		// Bounce the ball off the player
		Vector2 NormalOfCollision = ((Vector2)(transform.position - collid.transform.position)).normalized;
		MoveDirection = Vector2.Reflect(MoveDirection, NormalOfCollision);
	}
}
