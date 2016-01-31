using UnityEngine;
using System.Collections;

public class MouseCursorScript : MonoBehaviour {

    public static MouseCursorScript sngl;
        
    void Awake()
    {
        sngl = this;
    }

	// Use this for initialization
	void Start () {
        // Hide the real cursor
#if UNITY_EDITOR
#else
        Cursor.visible = false;
#endif
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10; ;
	}
}
