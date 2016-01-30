using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public static CameraShake sngl;


    Animator anim;
	// Use this for initialization

    void Awake()
    {
        anim = GetComponent<Animator>();
        sngl = this;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        anim.SetTrigger("shake");
    }
}
