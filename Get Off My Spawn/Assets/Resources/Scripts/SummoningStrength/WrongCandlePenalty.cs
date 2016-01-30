using UnityEngine;
using System.Collections;

public class WrongCandlePenalty : MonoBehaviour, ISumonQualityPenalty {

	public float PenaltyPerCandle = 0.1f;

	float CurrentPenalty;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void WrongCandleLit()
	{
		CurrentPenalty = PenaltyPerCandle;
	}

	// This consumes the penalty - not nice as Get => that the method doesn't mutate anything
	// may need to be revisited
	public float GetCurrentPenalty()
	{
		float Penalty = CurrentPenalty;
		CurrentPenalty = 0.0f;
		return Penalty;
	}
}
