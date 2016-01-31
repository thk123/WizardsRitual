using UnityEngine;
using System.Collections;

public class DemonSmash : MonoBehaviour {

	public float HitDamage;
	public float HitPeriod;

	Building TargetBuilding;
	IEnumerator HitBuildingCoroutine;

	// Use this for initialization
	void Start () {
		TargetBuilding = null;
		HitBuildingCoroutine = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D collid)
	{
		if(collid.tag == Utility.BuildingsTag)
		{
			TargetBuilding = collid.GetComponent<Building>();
			HitBuildingCoroutine = HitBuilding();
			StartCoroutine(HitBuildingCoroutine);
		}
	}

	void OnTriggerLeave2D(Collider2D collid)
	{
		if(collid.GetComponent<Building>() == TargetBuilding)
		{
			StopCoroutine(HitBuildingCoroutine);
		}
	}

	IEnumerator HitBuilding()
	{
		while(TargetBuilding != null)
		{
			TargetBuilding.Hit(HitDamage);

			yield return new WaitForSeconds(HitPeriod);
		}
		//return null;
	}
}
