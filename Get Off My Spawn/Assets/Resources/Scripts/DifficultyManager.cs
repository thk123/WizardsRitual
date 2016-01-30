using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

	//lic DifficultyScriptableObject StartingDifficulty;

	public Summoner summoner;
	public CandlePatternGenerator PatternGenerator;
	public HazardSpawner hazard;

	public List<DifficultyScriptableObject> DifficultySequence;

	// Use this for initialization
	void Start () {
		NextDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextDifficulty()
	{
		if(DifficultySequence.Count > 0)
		{
			LoadDifficulty(DifficultySequence[0]);
			DifficultySequence.RemoveAt(0);
		}
	}

	public void LoadDifficulty(DifficultyScriptableObject DifficultyToLoad)
	{
		if(summoner != null)
		{
			summoner.GetComponent<SummoningTimeDecay>().DecayRate = DifficultyToLoad.SumonerTimeDecayRate;
			summoner.GetComponent<WrongCandlePenalty>().PenaltyPerCandle = DifficultyToLoad.SumonerWrongCandlePenality;
			summoner.GetComponent<ExtinguishedCandlePenalty>().PenaltyPerCandle = DifficultyToLoad.ExtinguishedCandlePenalty;
			summoner.GetComponent<CorrectCandleBonus>().PenaltyPerCandle = DifficultyToLoad.CorrectCandleBonus;

			PatternGenerator.candle_num = DifficultyToLoad.NumberOfCandles;
			PatternGenerator.circle_num = DifficultyToLoad.NumberOfCircles;
			PatternGenerator.tot_radius = DifficultyToLoad.Tot_radius;

			// HAZARD CODE GOES HERE
			PatternGenerator.Restart();
		}
	}
}
