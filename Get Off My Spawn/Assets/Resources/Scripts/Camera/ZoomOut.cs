using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class ZoomOut : MonoBehaviour {

	public float TargetZoom;
	public AnimationCurve InterpolationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
	public float ZoomTime = 1.0f;

	float CurrentTime;
	float StartingSize;

	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		Restart();
		print(camera);
	}

	public void Restart()
	{
		CurrentTime = 0.0f;
		StartingSize = camera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTime += Time.deltaTime;

		if(CurrentTime <= ZoomTime)
		{
			float NewSize = Mathf.Lerp(StartingSize, TargetZoom, InterpolationCurve.Evaluate(CurrentTime / ZoomTime));
			camera.orthographicSize = NewSize;
		}
		else
		{
			enabled = false;
		}

	}
}
