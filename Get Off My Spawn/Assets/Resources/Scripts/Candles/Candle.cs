using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

	// Use this for initialization
	bool IsCandleLit;
    bool IsFrozen;

	public AudioSource GoodSound;
	public AudioSource BadSound;

	public CandlePatternGenerator.candle_pos CandlePosition
	{
		get;
		private set;
	}

	public delegate void CandleEventHandler(Candle Sender);
	public event CandleEventHandler OnCandleLit;
	public event CandleEventHandler OnCandleExtinguished;

	bool CanUnLight;

    SpriteRenderer candle_flame;
    GameObject candle_glow;

    void Start () {
		IsCandleLit = false;
        IsFrozen = false;
        Summoner.SummonSuccess += FreezeLit;
        candle_flame = transform.FindChild("CandleFlame").GetComponent<SpriteRenderer>();
        candle_glow = transform.FindChild("CandleGlow").gameObject;
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
        if (IsLit == IsCandleLit || IsFrozen)
            return;
		IsCandleLit = IsLit;
		if(IsCandleLit)
		{
            candle_flame.enabled = true;
            candle_glow.SetActive(true);
			if(OnCandleLit != null) { OnCandleLit(this); }
		}
		else
		{
            candle_flame.enabled = false;
            candle_glow.SetActive(false);
			if(OnCandleExtinguished != null) { OnCandleExtinguished(this); }
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
	public void CorrectCandle()
	{
		GoodSound.Play();
	}
	public void IncorrectCandle()
	{
		BadSound.Play();
	}

    public void FreezeLit()
    {
        IsFrozen = true;
    }
}
