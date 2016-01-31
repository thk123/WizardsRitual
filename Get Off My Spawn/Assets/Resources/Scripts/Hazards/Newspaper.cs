using UnityEngine;
using System.Collections;

public class Newspaper : Hazard {

	Vector2 MoveDirection;

	public float Speed = 6.0f;

	public float AngleRange = 20.0f;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		Vector2 StartingPosition = PickStartPosition();
		transform.position = StartingPosition;

		float RandomAngle = Random.Range(-AngleRange, AngleRange);
		var Rotator = Quaternion.AngleAxis(RandomAngle, Vector3.forward);
		MoveDirection = Rotator * ((-StartingPosition).normalized);
		Debug.DrawLine(StartingPosition, StartingPosition + 5.0f * MoveDirection, Color.white, 1.0f);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
        // var Rotator = Quaternion.AngleAxis(5, Vector3.forward);
        // transform.localRotation = transform.localRotation * Rotator;
        rbody.velocity = MoveDirection * Speed;
	}

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (collider.tag == Utility.PlayerTag)
        {
            Vector2 NormalOfCollision = ((Vector2)(transform.position - collider.transform.position)).normalized;
            MoveDirection = Vector2.Reflect(MoveDirection, NormalOfCollision);
//            if (!GetComponent<AudioSource>().isPlaying)
//            {
//                GetComponent<AudioSource>().Play();
//            }
        }
    }

    protected override void OnPlayerCollision(Collider2D Player)
    {
        base.OnPlayerCollision(Player);
    }
}