using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISumonQualityPenalty
{
	float GetCurrentPenalty();
}

public class Summoner : MonoBehaviour {
    
    public static Summoner sngl;
	List<ISumonQualityPenalty> Penalties;
    public delegate void SummonEvent();
    public static SummonEvent SummonSuccess;

	public float SummonQuality
	{
		get;
		private set;
	}

	// Use this for initialization

    void Awake()
    {
        sngl = this;
    }

	void Start () {
		SummonQuality = 1.0f;
		Penalties = new List<ISumonQualityPenalty>();
		Penalties.AddRange(gameObject.GetComponents<ISumonQualityPenalty>());
	}
	
	// Update is called once per frame
	void Update () {
		foreach(ISumonQualityPenalty Penalty in gameObject.GetComponents<ISumonQualityPenalty>())
		{
			SummonQuality -= Penalty.GetCurrentPenalty();	
			SummonQuality = Mathf.Clamp01(SummonQuality);
		}
	}

    public void WrongCandleLit()
    {
        // Shake the camera!
        CameraShake.sngl.Play();
        foreach (ISumonQualityPenalty pen in Penalties)
        {
            if (pen is WrongCandlePenalty)
                ((WrongCandlePenalty)pen).WrongCandleLit();
        }
    }

    public void ExtinguishedCandle()
    {
        foreach (ISumonQualityPenalty pen in Penalties)
        {
            if (pen is ExtinguishedCandlePenalty)
                ((ExtinguishedCandlePenalty)pen).ExtinguishedCandle();
        }
    }

    public void CorrectCandle()
    {
        foreach (ISumonQualityPenalty pen in Penalties)
        {
            if (pen is CorrectCandleBonus)
                ((CorrectCandleBonus)pen).CorrectCandle();
        }
    }

    public void Summon()
	{
		print("SUMMONING A LEVEL " + (SummonQuality * 100.0f).ToString("0") + " DEMON!");
        if (SummonSuccess != null)
            SummonSuccess();

        FindObjectOfType<DifficultyManager>().NextDifficulty();
        // Also destroy every Hazard
        /*
        Hazard[] all_hazards = GameObject.FindObjectsOfType<Hazard>();
        foreach (Hazard h in all_hazards)
        {
            Destroy(h.gameObject);
        }
        */

	}
}
