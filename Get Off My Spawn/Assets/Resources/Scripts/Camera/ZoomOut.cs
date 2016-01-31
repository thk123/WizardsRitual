using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class ZoomOut : MonoBehaviour {

	public float TargetZoom;
	public AnimationCurve InterpolationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
	public float ZoomTime = 1.0f;

	float CurrentTime;
	float StartingSize;
	float CurrentTargetSize;


	Camera zoomingCamera;

	void Awake()
	{
		zoomingCamera = GetComponent<Camera>();
	}

	// Use this for initialization
	void Start () {
		Restart();
	}

	public void Restart()
	{
		CurrentTime = 0.0f;

		CurrentTargetSize = TargetZoom;
		StartingSize = zoomingCamera.orthographicSize;
	}

	public void Reverse()
	{
		CurrentTime = 0.0f;

		CurrentTargetSize = StartingSize;
		StartingSize = zoomingCamera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTime += Time.deltaTime;

		if(CurrentTime <= ZoomTime)
		{
			float NewSize = Mathf.Lerp(StartingSize, CurrentTargetSize, InterpolationCurve.Evaluate(CurrentTime / ZoomTime));
			zoomingCamera.orthographicSize = NewSize;
		}
		else
		{
			enabled = false;
		}

	}
}
