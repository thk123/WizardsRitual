using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenuAttribute]
public class DifficultyScriptableObject : ScriptableObject {

	// Candle setup
	public int NumberOfCircles = 3;
	public int NumberOfCandles = 15;
	public float Tot_radius = 4.0f;

	// Summoner setup
	public float SumonerTimeDecayRate = 0.01f;
	public float SumonerWrongCandlePenality = 0.1f;
	public float ExtinguishedCandlePenalty = 0.1f;
	public float CorrectCandleBonus = -0.02f;

	// Hazard setup
	public float MeanPeriod = 1.5f;
	public float PeriodVariance = 1.0f;
	public int MaxNumberAcrossAllHazards = 4;

	public List<HazardDefinition> Hazards;
}
