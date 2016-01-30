using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

	// Use this for initialization
	bool IsCandleLit;

	public CandlePatternGenerator.candle_pos CandlePosition
	{
		get;
		private set;
	}

	public delegate void CandleEventHandler(Candle Sender);
	public event CandleEventHandler OnCandleLit;

	bool CanUnLight;

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
				print("lighting candle");
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
			if(OnCandleLit != null) { OnCandleLit(this); }
		}
		else
		{
			//GameObject.Destroy(gameObject);
			gameObject.GetComponent<SpriteRenderer>().color = Color.white; 
			print("Unlighing candle: " + gameObject.GetComponent<SpriteRenderer>().color.ToString());
		}
	}

	public bool GetIsCandleLit()
	{
		return IsCandleLit;
	}

	public void SetCandlePosition(CandlePatternGenerator.candle_pos Position)
	{
		CandlePosition = Position;
	}
}
