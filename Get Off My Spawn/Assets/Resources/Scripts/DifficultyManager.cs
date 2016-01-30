using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

	public DifficultyScriptableObject StartingDifficulty;

	public Summoner summoner;
	public CandlePatternGenerator PatternGenerator;
	public HazardSpawner hazard;

	// Use this for initialization
	void Awake () {
		if(StartingDifficulty != null)
		{
			LoadDifficulty(StartingDifficulty);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
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

		}
	}
}
