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

    SpriteRenderer candle_flame;

	void Start () {
		IsCandleLit = false;
        candle_flame = transform.FindChild("CandleFlame").GetComponent<SpriteRenderer>();
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
            candle_flame.enabled = true;
			if(OnCandleLit != null) { OnCandleLit(this); }
		}
		else
		{
            candle_flame.enabled = false;
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
