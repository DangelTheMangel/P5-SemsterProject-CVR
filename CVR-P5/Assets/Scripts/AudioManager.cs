using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Transform playerHead; //Assings your VR Camera angle here | Check Inspector
    public AudioSource[] audioSources; //Can assing all our audio sources here
    public float angleThreshold = 30f; // Angle threshold to hear the sound

    public Sound[] sounds;

    public static AudioManager instance;


    // Start is called before the first frame update
    void Awake ()
    {

        if (instance == null)
            instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    void Update()
    {
        // To get the direction the player is looking at
        Vector3 lookDirection = playerHead.forward;

        foreach (AudioSource source in audioSources)
        {
            // Calculates the direction from player to audio source
            Vector3 toSource = (source.transform.position - playerHead.position).normalized; 

            // Calculate angle between look direction and toSource direction
            float angle = Vector3.Angle(lookDirection, toSource);

            // If the angle is less than the threshold, and the audio source is not playing, play it
            if (angle < angleThreshold)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
            else
            {
                // Otherwise, stop the audio if it's playing
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }
        }
    }
        



    

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
       //If you misspell the name of the audio sound, it won't try to play the sound. Thus, clearing the potential error
       if (s == null)
       {
        Debug.LogWarning("Sound: " + name  + "not found!");
       }
            return;     

       s.source.Play();
    }

}
