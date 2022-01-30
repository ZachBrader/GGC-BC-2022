using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public static void PlayIfNotPlaying(AudioSource source, AudioClip clip)
    {
        //do nothing if the source is already playing
        if (source.isPlaying)
        {
            return;
        }

        source.clip = clip;
        source.Play();
    }
}
