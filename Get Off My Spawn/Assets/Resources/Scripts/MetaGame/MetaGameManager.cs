using UnityEngine;
using System.Collections;

public class MetaGameManager : MonoBehaviour {

	public Summoner sumoner;

	public PhysicsPlayerControl Wizard;
	public PhysicsPlayerControl Demon;
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

		SetBoundariesEnabled(false);

		Wizard.gameObject.active = false;
		Demon.gameObject.active = true;
	}

	private void EndMetaGame()
	{
		Wizard.gameObject.active = true;
		Demon.gameObject.active = false;

		ZoomOut ZoomComponent = Camera.main.GetComponent<ZoomOut>();
		if(ZoomComponent != null)
		{
			ZoomComponent.Reverse();
			ZoomComponent.enabled = true;
		}

		SetBoundariesEnabled(true);

		FindObjectOfType<DifficultyManager>().NextDifficulty();  

		enabled = false;
	}

	private void SetBoundariesEnabled(bool Enabled)
	{
		var Boundaries = GameObject.FindGameObjectsWithTag(Utility.BoundsTag);
		foreach(var Boundary in Boundaries)
		{
			Boundary.GetComponent<Collider2D>().enabled = Enabled;
		}
	}
}
