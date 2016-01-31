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
			TargetBuilding = GetBuildingFromCollider(collid);
			if(TargetBuilding == null)
			{
				Debug.LogWarning("Building component not found on the parent of the collider tagged with building: " + collid.name);
			}
			HitBuildingCoroutine = HitBuilding();
			StartCoroutine(HitBuildingCoroutine);
		}
	}

	void OnTriggerExit2D(Collider2D collid)
	{
		if(GetBuildingFromCollider(collid) == TargetBuilding)
		{
			StopCoroutine(HitBuildingCoroutine);
		}
	}

	Building GetBuildingFromCollider(Collider2D collid)
	{
		return collid.GetComponentInParent<Building>();	
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
