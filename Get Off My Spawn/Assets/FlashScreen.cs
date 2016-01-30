using UnityEngine;
using System.Collections;

public class FlashScreen : MonoBehaviour {

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Summoner.SummonSuccess += FlashMe;
    }

    void FlashMe()
    {
        anim.SetTrigger("flash");
    }
}
