using UnityEngine;
using System.Collections;

public class Dog : Hazard {

	Chase_Player c;

	public float BarkPeriod = 6.0f;
	public float BarkVariance = 1.0f;
	public float MinGap = 2.0f;

	// Use this for initialization
	protected override void Start () {
		base.Start();	
		c = GetComponent<Chase_Player> ();
		//if (Random.Range (0, 5)> 0) {
			c.target = GameObject.FindGameObjectWithTag (Utility.PlayerTag).transform;
	//	} else {
	//		c.target = GameObject.FindObjectOfType<Hazard> ().transform;
//		}
		transform.position = PickStartPosition ();

		StartCoroutine(Bark());
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	
	}

	IEnumerator Bark()
	{
		while(true)
		{
			float NextPause = Mathf.Max(Utility.NextGaussianDouble(BarkPeriod, BarkVariance), MinGap);
			yield return new WaitForSeconds(NextPause);
			if(!GetComponent<AudioSource>().isPlaying)
			{
				if(!GetComponent<Chase_Player>().IsJumping())
				{
					GetComponent<AudioSource>().Play();
				}
			}
			else
			{
				Debug.LogWarning("Warning: Min gap between barks was too little");
			}
		}
	}
}
