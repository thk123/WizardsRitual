using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class FocusProgressBarController : MonoBehaviour {

	public PlayerFocus Focus;

	Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = Focus.CurrentFocus;
	}
}
