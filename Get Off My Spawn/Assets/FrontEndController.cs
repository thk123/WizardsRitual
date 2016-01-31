using UnityEngine;
using System.Collections;

public class FrontEndController : MonoBehaviour {

	public string MainGameLevel;
	public GameObject CreditsUiElement;

	// Use this for initialization
	void Start () {

        Cursor.visible = true;
	
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
		CreditsUiElement.active = true;
	}

	public void HideCredits()
	{
		CreditsUiElement.active = false;	
	}

	public void ExitGame()
	{
		Application.Quit();
	}

}
