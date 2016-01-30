using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardSpawner : MonoBehaviour {

	public List<HazardDefinition> Hazards;

	public float MeanFrequency;
	public float FrequencyVariance;
	public float MinGap = 0.1f;

    float prob_norm;

	// Use this for initialization
	void Start () {
        // Calculate probability normalization factor
        prob_norm = 0.0f;
        foreach (HazardDefinition hdef in Hazards)
        {
            prob_norm += hdef.probability;
        }

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
        // Pick which one
        float cum_prob = 1e-9f; // Just a safety measure
        float randomPicker = Random.value * prob_norm;
		int RandomObstanceIndex = 0;
        foreach (HazardDefinition hdef in Hazards)
        {
            cum_prob += hdef.probability;
            if (cum_prob > randomPicker)
                break;
            RandomObstanceIndex++;
        }
		Instantiate(Hazards[RandomObstanceIndex].prefab);
	}
}
