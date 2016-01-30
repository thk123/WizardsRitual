using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public float WizardImpactStrength = 1.0f;

	protected Rigidbody2D rbody;

	private Vector3 TopLeftCorner;
	private Vector3 BottomRightCorner;

	protected bool FullyEnteredGarden;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	protected virtual void Start () {
        Summoner.SummonSuccess += KillMe;
		FullyEnteredGarden = false;
		ComputeGameBounds(out TopLeftCorner, out BottomRightCorner);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(!FullyEnteredGarden)
		{
			if(ContainedInBounds())
			{
				SetFullyEnteredGarden(true);
			}
		}
		else
		{
			if(!ContainedInBounds())
			{
				SetFullyEnteredGarden(false);
			}
		}
	}

	protected virtual void SetFullyEnteredGarden(bool Entered)
	{
		// If we had entered the garden and we've now left we can destroy
		if(FullyEnteredGarden && !Entered)
		{
			GameObject.Destroy(gameObject);
		}
		FullyEnteredGarden = Entered;
	}

	protected Vector2 PickStartPosition()
	{
		int Side =Random.Range(0, 4);
		int xPos = Random.Range(0, Screen.width);	
		int yPos = Random.Range(0, Screen.height);

		Vector2 ScreenSpaceVector = Vector2.zero;

		switch(Side)
		{
			case 0:
				ScreenSpaceVector = new Vector2(xPos, 0);
				break;

			case 1:
				ScreenSpaceVector = new Vector2(xPos, Screen.height);
				break;

			case 2:
				ScreenSpaceVector = new Vector2(0, yPos);
				break;

			case 3:
				ScreenSpaceVector = new Vector2(Screen.width, yPos);
				break;

		}
		
		return Camera.main.ScreenToWorldPoint(ScreenSpaceVector);
	}

	protected bool ContainedInBounds()
	{
		return transform.position.x >= TopLeftCorner.x && transform.position.x <= BottomRightCorner.x
				&& transform.position.y >= BottomRightCorner.y && transform.position.y <= TopLeftCorner.y;
	}

	private void ComputeGameBounds(out Vector3 TopLeft, out Vector3 BottomRight)
	{
		GameObject[] Bounds = GameObject.FindGameObjectsWithTag(Utility.BoundsTag);
		float LeftValue = Mathf.Infinity;
		float RightValue = -Mathf.Infinity;

		float TopValue = -Mathf.Infinity;
		float BottomValue = Mathf.Infinity;
		foreach(GameObject boundingObject in Bounds)
		{
			Bounds bounds = boundingObject.GetComponent<Collider2D>().bounds;

			// The horizontal is biggger than the vertical so is a top/bottom bound
			if(bounds.extents.x > bounds.extents.y)
			{
				TopValue = Mathf.Max(bounds.min.y, TopValue);
				BottomValue = Mathf.Min(bounds.max.y, BottomValue);
			}
			else // the vertical is bigger than the horizontal, so is a left/right bound
			{
				LeftValue = Mathf.Min(bounds.max.x, LeftValue);
				RightValue = Mathf.Max(bounds.min.x, RightValue);
			}
		}

		TopLeft = new Vector3(LeftValue, TopValue, 0.0f);
		BottomRight = new Vector3(RightValue, BottomValue, 0.0f);
	}

	protected virtual void OnTriggerEnter2D(Collider2D collid)
	{
		if(collid.tag == Utility.PlayerTag)
		{
			OnPlayerCollision(collid);
		}
	}

	protected virtual void OnPlayerCollision(Collider2D Player)
	{

	}

    protected void KillMe()
    {
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        Summoner.SummonSuccess -= KillMe;
    }
}
