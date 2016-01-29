using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardSpawner : MonoBehaviour {

	public List<Hazard> Hazards;

	public float MeanFrequency;
	public float FrequencyVariance;
	public float MinGap = 0.1f;

	// Use this for initialization
	void Start () {
		if(Hazards.Count > 0)
		{
			StartCoroutine(SpawnRandomHazard());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnRandomHazard()
	{
		while(true)
		{
			float NextPause = Mathf.Max(Utility.NextGaussianDouble(MeanFrequency, FrequencyVariance), MinGap);
			yield return new WaitForSeconds(NextPause);
	
			SpawnNextObstacle();
		}
	}

	void SpawnNextObstacle()
	{
		int RandomObstanceIndex = Random.Range(0, Hazards.Count);
		Instantiate(Hazards[RandomObstanceIndex]);
	}
}
