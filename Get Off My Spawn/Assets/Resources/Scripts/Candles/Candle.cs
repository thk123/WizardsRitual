using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

	// Use this for initialization
	bool IsCandleLit;

	void Start () {
		IsCandleLit = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D Collider)
	{
		if(Collider.tag == Utility.PlayerTag)
		{
			if(!IsCandleLit)
			{
				SetCandleLit(true);
			}
		}
	}

	public void SetCandleLit(bool IsLit)
	{
		IsCandleLit = IsLit;
		if(IsCandleLit)
		{
			gameObject.GetComponent<SpriteRenderer>().color = Color.yellow; 
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().color = Color.white; 	
		}
	}

	public bool GetIsCandleLit()
	{
		return IsCandleLit;
	}
}
