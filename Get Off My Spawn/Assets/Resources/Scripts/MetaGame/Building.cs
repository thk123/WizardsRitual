using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public float BuildingHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(BuildingHealth <= 0.0f)
		{
			GameObject.Destroy(gameObject);
		}
	}

	public void Hit(float damage)
	{
		BuildingHealth -= damage;
	}

}
