﻿using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static float NextGaussianDouble(float Mean, float Variance)
	{
    	float u, v, S;
	
    	do
    	{
    	    u = 2.0f * Random.value - 1.0f;
    	    v = 2.0f * Random.value - 1.0f;
    	    S = u * u + v * v;
    	}
    	while (S >= 1.0);
	
    	float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
    	return Mean + (u * fac * Variance);
	}
}
