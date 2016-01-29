using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

	// Use this for initialization
	bool IsCandleLit;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEntrer2D(Collider2D Collider)
	{
		print("Collided")

		if(Collider.tag == Utility.PlayerTag)
		{
			if(!IsCandleLit)
			{
				IsCandleLit = true;
			}
		}
	}

	bool GetIsCandleLit()
	{
		return IsCandleLit;
	}
}
