using UnityEngine;
using System.Collections;

[System.Serializable]
public class HazardDefinition : System.Object {

    public Hazard prefab;
    public float probability;
    public int MaxNumberOnScreen;

    public HazardDefinition(float prob, int max)
    {
    	probability = prob;
    	MaxNumberOnScreen = max;
    }

}
