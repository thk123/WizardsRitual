using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISumonQualityPenalty
{
	float GetCurrentPenalty();
}

public class Summoner : MonoBehaviour {

	List<ISumonQualityPenalty> Penalties; 

	public float SummonQuality
	{
		get;
		private set;
	}

	// Use this for initialization
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
			SummonQuality = Mathf.Clamp(SummonQuality, 0.0f, 1.0f);
		}
	}

	public void Summon()
	{
		print("SUMMONING A LEVEL " + (SummonQuality * 100.0f).ToString("0") + " DEMON!");
	}
}
