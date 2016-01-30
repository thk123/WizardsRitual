using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public float WizardImpactStrength = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void OnTriggerEnter2D(Collider2D collid)
	{
		GetComponent<AudioSource>().Play();
	}
}
