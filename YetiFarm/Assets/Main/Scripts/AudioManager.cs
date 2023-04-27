using UnityEngine.Audio;
using System;
using UnityEngine;

/*
 * Take the AudioManager prefab to your scene and add in the audio clips you are going to use.
 * Add the sounds clips and their names to the AudioManager for your game.
 * Add a reference to scripts that play a sound often or just find the object of type and call the PlaySound method like so:
 * audioManager.PlaySound(clipName you want to be played);
 */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == clipName);
        if (s == null) //in case the sound doesn't exist
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopSound(string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == clipName);
        if (s == null) //in case the sound doesn't exist
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }
        s.source.Stop();
    }
}
