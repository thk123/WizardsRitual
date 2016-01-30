using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class SumonQualityController : MonoBehaviour {

	public Summoner Summoner; 
	
	Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
	
		if(Summoner == null)
		{
			Summoner = FindObjectOfType<Summoner>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = Summoner.SummonQuality;
	}
}
