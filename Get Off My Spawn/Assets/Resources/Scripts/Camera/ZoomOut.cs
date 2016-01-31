using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class ZoomOut : MonoBehaviour {

	public float TargetZoom;
    public Vector3 TargetCenter;

    public AnimationCurve InterpolationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
	public float ZoomTime = 1.0f;
	float CurrentTime;
	float StartingSize;
    Vector3 StartingCenter;
	float CurrentTargetSize;
    Vector3 CurrentTargetCenter;

	Camera zoomingCamera;
    Transform cartTransform;

	void Awake()
	{
		zoomingCamera = GetComponent<Camera>();
        cartTransform = transform.parent.GetComponent<Transform>();
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
        CurrentTargetCenter = TargetCenter;
        StartingCenter = cartTransform.position;
	}

	public void Reverse()
	{
		CurrentTime = 0.0f;

		CurrentTargetSize = StartingSize;
		StartingSize = zoomingCamera.orthographicSize;
        CurrentTargetCenter = StartingCenter;
        StartingCenter = cartTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTime += Time.deltaTime;

		if(CurrentTime <= ZoomTime)
		{
            float t = InterpolationCurve.Evaluate(CurrentTime / ZoomTime);

            zoomingCamera.orthographicSize = Mathf.Lerp(StartingSize, CurrentTargetSize, t);
            cartTransform.position = Vector3.Lerp(StartingCenter, CurrentTargetCenter, t);
		}
		else
		{
			enabled = false;
		}

	}
}
