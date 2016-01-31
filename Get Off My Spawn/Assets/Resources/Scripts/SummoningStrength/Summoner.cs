using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public interface ISumonQualityPenalty
{
	float GetCurrentPenalty();
}

public class Summoner : MonoBehaviour {
    
    public enum GameState { RUNNING, PAUSED, WON};

    public static Summoner sngl;
	List<ISumonQualityPenalty> Penalties;
    public delegate void SummonEvent();
    public static SummonEvent SummonSuccess;

    public GameState myState;

	public float SummonQuality
	{
		get;
		private set;
	}

	// Use this for initialization

    void Awake()
    {
        sngl = this;
    }

	void Start () {
		
		Penalties = new List<ISumonQualityPenalty>();
		Penalties.AddRange(gameObject.GetComponents<ISumonQualityPenalty>());

        Restart();
	}

    public void Restart()
    {
        SummonQuality = 1.0f;
        Pause();
        // Clear any existing demons
        DemonSmash[] any_demon = GameObject.FindObjectsOfType<DemonSmash>();
        foreach (DemonSmash d in any_demon)
        {
            Destroy(d.gameObject);
        }
        EndText.sngl.Hide();
        // Start the countdown, once it's over, shit's going down
        Countdown.sngl.onEnd = Unpause;
        Countdown.sngl.Reset();
    }

    // Update is called once per frame
    void Update () {

        switch (myState)
        {
            case GameState.RUNNING:
                foreach (ISumonQualityPenalty Penalty in gameObject.GetComponents<ISumonQualityPenalty>())
                {
                    SummonQuality -= Penalty.GetCurrentPenalty();
                    SummonQuality = Mathf.Clamp01(SummonQuality);
                }
                break;
            case GameState.WON:

                if (Input.GetMouseButtonDown(0))
                {
                    MetaGameManager MetaGame = FindObjectOfType<MetaGameManager>();
                    if (MetaGame != null)
                    {
                        MetaGame.StartMetaGame();
                        // TODO: if we want a different decay rate for the demon then we
                        // should change the ISumonQualityPenalty here
                    }
                    else
                    {
                        FindObjectOfType<DifficultyManager>().NextDifficulty();
                    }

                }

                if(Input.GetKeyDown(KeyCode.R))
                {
                     FindObjectOfType<DifficultyManager>().RestartDifficulty();
                }


                break;
            default:
                break;
        }
    }

    public void WrongCandleLit()
    {
        // Shake the camera!
        CameraShake.sngl.Play();
        foreach (ISumonQualityPenalty pen in Penalties)
        {
            if (pen is WrongCandlePenalty)
                ((WrongCandlePenalty)pen).WrongCandleLit();
        }
    }

    public void ExtinguishedCandle()
    {
        foreach (ISumonQualityPenalty pen in Penalties)
        {
            if (pen is ExtinguishedCandlePenalty)
                ((ExtinguishedCandlePenalty)pen).ExtinguishedCandle();
        }
    }

    public void CorrectCandle()
    {
        foreach (ISumonQualityPenalty pen in Penalties)
        {
            if (pen is CorrectCandleBonus)
                ((CorrectCandleBonus)pen).CorrectCandle();
        }
    }

    public void Summon()
	{
        int lv = Mathf.CeilToInt(SummonQuality * 100.0f);

        print("SUMMONING A LEVEL " + lv + " DEMON!");
        EndText.sngl.Show( "Summoning a level " + lv + " demon!");

        GetComponent<AudioSource>().Play();
        if (SummonSuccess != null)
            SummonSuccess();

        myState = GameState.WON;

        //
        // Also destroy every Hazard
        /*
        Hazard[] all_hazards = GameObject.FindObjectsOfType<Hazard>();
        foreach (Hazard h in all_hazards)
        {
            Destroy(h.gameObject);
        }
        */

	}

    public void Pause()
    {
        if (myState != GameState.PAUSED)
        {
            myState = GameState.PAUSED;
            Time.timeScale = 0.0f;
        }
    }

    public void Unpause()
    {
        if (myState != GameState.RUNNING)
        {
            myState = GameState.RUNNING;
            Time.timeScale = 1.0f;
        }
    }
}
