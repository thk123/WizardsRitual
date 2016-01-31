using UnityEngine;
using System.Collections;

public class IsometricBehaviour : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y*0.01f);
	}
}
