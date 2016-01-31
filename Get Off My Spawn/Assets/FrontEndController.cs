using UnityEngine;
using System.Collections;

public class FrontEndController : MonoBehaviour {

	public string MainGameLevel;
	public string CreditsLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame()
	{
		Application.LoadLevel(MainGameLevel);
	}

	public void Credits()
	{
		Application.LoadLevel(CreditsLevel);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

}
