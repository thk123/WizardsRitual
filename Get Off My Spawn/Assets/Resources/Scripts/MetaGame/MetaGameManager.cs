using UnityEngine;
using System.Collections;

public class MetaGameManager : MonoBehaviour {

	public Summoner sumoner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(sumoner.SummonQuality <= 0.0f)
		{
			EndMetaGame();
		}
	}

	public void StartMetaGame()
	{
		enabled = true;
		ZoomOut ZoomComponent = Camera.main.GetComponent<ZoomOut>();
		if(ZoomComponent != null)
		{
			ZoomComponent.Restart();
			ZoomComponent.enabled = true;
		}

		var Boundaries = GameObject.FindGameObjectsWithTag(Utility.BoundsTag);
		foreach(var Boundary in Boundaries)
		{
			Boundary.GetComponent<Collider2D>().enabled = false;
		}
	}

	private void EndMetaGame()
	{
		ZoomOut ZoomComponent = Camera.main.GetComponent<ZoomOut>();
		if(ZoomComponent != null)
		{
			ZoomComponent.Reverse();
			ZoomComponent.enabled = true;
		}

		enabled = false;
	}
}
