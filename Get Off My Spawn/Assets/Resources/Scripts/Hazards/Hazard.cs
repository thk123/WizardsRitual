using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public float WizardImpactStrength = 1.0f;

	protected Rigidbody2D rbody;


    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected Vector2 PickStartPosition()
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

	protected virtual void OnTriggerEnter2D(Collider2D collid)
	{
		GetComponent<AudioSource>().Play();

		if(collid.tag == Utility.PlayerTag)
		{
			OnPlayerCollision(collid);
		}
	}

	protected virtual void OnPlayerCollision(Collider2D Player)
	{

	}
}
