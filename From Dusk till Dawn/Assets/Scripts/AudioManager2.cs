using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{

    public AudioSource happy;
    public AudioSource sad;
    private AudioSource currentAudio;

    // Use this for initialization
    void Start()
    {

        if (GameStateManager.dream2Completed)
        {
            currentAudio = happy;
        }
        else
        {
            currentAudio = sad;
        }

        currentAudio.loop = true;
        currentAudio.Play();

    }

}
