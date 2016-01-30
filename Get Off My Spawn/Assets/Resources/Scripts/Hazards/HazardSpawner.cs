using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Type = System.Type;

public class HazardSpawner : MonoBehaviour {

	public List<HazardDefinition> Hazards;

	public float MeanFrequency;
	public float FrequencyVariance;
	public float MinGap = 0.1f;

	public int MaxNumberAcrossAllHazards = 4;

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
	
			if(CountNumberOfHazard(typeof(Hazard)) < MaxNumberAcrossAllHazards)
			{
				SpawnNextObstacle();
			}
		}
	}

	void SpawnNextObstacle()
	{
        // Pick which one
        Hazard HazardToSpawn = null;
        List<HazardDefinition> PotentialHazards = new List<HazardDefinition>(Hazards);

        while(HazardToSpawn == null && PotentialHazards.Count > 0)
        {
        	float cum_prob = 1e-9f; // Just a safety measure
        	float randomPicker = Random.value * ComputeTotalProbability(PotentialHazards);
			int RandomObstanceIndex = 0;
        	foreach (HazardDefinition hdef in PotentialHazards)
        	{
        	    cum_prob += hdef.probability;
        	    if (cum_prob > randomPicker)
        	        break;
        	    RandomObstanceIndex++;
        	}
	
        	Type TypeOfHazard = GetTypeOfHazard(PotentialHazards[RandomObstanceIndex].prefab);
        	int NumberAlready = CountNumberOfHazard(TypeOfHazard);

        	if(NumberAlready < PotentialHazards[RandomObstanceIndex].MaxNumberOnScreen)
			{
				HazardToSpawn = PotentialHazards[RandomObstanceIndex].prefab;
			}
			else
			{
				PotentialHazards.RemoveAt(RandomObstanceIndex);
				// we skip this round - there are already too many
			}
		} 
		if(HazardToSpawn != null)
		{
			Instantiate(HazardToSpawn);
		}
	}

	Type GetTypeOfHazard(Hazard HazardPrefab)
	{
		return HazardPrefab.GetType();
	}

	int CountNumberOfHazard(Type HazardType)
	{
		var HazardsOfType = GameObject.FindObjectsOfType(HazardType);
		return HazardsOfType.Length;
	}

	float ComputeTotalProbability(IEnumerable<HazardDefinition> ActiveHazards)
	{
		float prob_norm = 0.0f;
		foreach (HazardDefinition hdef in ActiveHazards)
        {
            prob_norm += hdef.probability;
        }

        return prob_norm;
	}
}
