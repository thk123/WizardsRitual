using UnityEngine;
using System.Collections;

public class SummoningTimeDecay : MonoBehaviour, ISumonQualityPenalty{

	public float DecayRate = 0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetCurrentPenalty()
	{
		return Time.deltaTime * DecayRate;
	}
}
