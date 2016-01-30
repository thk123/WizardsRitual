using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemonChargeBar : MonoBehaviour {

    public Summoner summoner;

    Image bar_image;

	// Use this for initialization
	void Awake () {
        bar_image = GetComponent<Image>();
	}

    void Start()
    {
        if (summoner == null)
        {
            summoner = GameObject.FindObjectOfType<Summoner>();
        }
    }

    void Update()
    {
        bar_image.fillAmount = summoner.SummonQuality;
    }

}
