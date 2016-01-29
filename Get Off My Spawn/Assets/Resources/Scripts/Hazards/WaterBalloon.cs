using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour {

	Vector2 Target;

	public float Speed = 10.0f;


	// Use this for initialization
	void Start () {
		Target = Camera.main.ScreenToWorldPoint(PickTarget());

		transform.position = Camera.main.ScreenToWorldPoint(PickStartPosition());
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 MovementVector = (Target - ((Vector2)transform.position)).normalized;
		transform.position += ((Vector3)MovementVector) * Time.deltaTime * Speed;
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
