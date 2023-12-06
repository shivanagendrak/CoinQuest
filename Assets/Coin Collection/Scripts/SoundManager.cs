using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource coinCollectSound;
    public AudioSource winSound;
    public AudioSource loseSound;
    public AudioSource healthSound;
    public AudioSource jumpSound;

    private void Awake()
    {
        Instance = this;
    }


}
