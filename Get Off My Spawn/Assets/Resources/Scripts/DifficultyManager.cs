using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

	//lic DifficultyScriptableObject StartingDifficulty;

	public Summoner summoner;
	public CandlePatternGenerator PatternGenerator;
	public HazardSpawner hazardSpawner;

	public List<DifficultyScriptableObject> DifficultySequence;

	public int LoopingLastNLevels = 1;
	int CurrentLevel = 0;

	// Use this for initialization
	void Start () {
		LoadDifficulty(DifficultySequence[0]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextDifficulty()
	{
		if(CurrentLevel < DifficultySequence.Count)
		{
			++CurrentLevel;
			LoadDifficulty(DifficultySequence[CurrentLevel]);
			
		}
		else
		{
			int LevelToLoop = Random.Range(DifficultySequence.Count - LoopingLastNLevels, DifficultySequence.Count);
			CurrentLevel = LevelToLoop;
			LoadDifficulty(DifficultySequence[LevelToLoop]);
		}
	}

	public void RestartDifficulty()
	{
		LoadDifficulty(DifficultySequence[CurrentLevel]);
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

			// HAZARDSpawner CODE GOES HERE
			hazardSpawner.MeanFrequency = DifficultyToLoad.MeanPeriod;
			hazardSpawner.FrequencyVariance = DifficultyToLoad.PeriodVariance;
			hazardSpawner.MaxNumberAcrossAllHazards = DifficultyToLoad.MaxNumberAcrossAllHazards;

			hazardSpawner.Hazards = DifficultyToLoad.Hazards;


			PatternGenerator.Restart();
			hazardSpawner.Restart();
			summoner.Restart();
		}
	}
}
