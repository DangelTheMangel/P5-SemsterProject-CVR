using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioListenerManager : MonoBehaviour
{
    [SerializeField]
    AudioSource[] SFX_AudioSources;
    public float maxVolume = 1.0f; // The maximum volume the audio source should reach
    public float angleToMaxVolume = 15f; // Angle at which the audio will play at maxVolume
    [SerializeField]
    AudioSource musicAudioSources;
    [SerializeField]
    AudioClip[] MusicTracks;
    [SerializeField]
    AudioSource musicPlayer;
    [SerializeField]
    float hearingRange = 10;
    [SerializeField]
    Color hearingGizmoColor;
    private string AudiologTag = "AudioLog";


    // Start is called before the first frame update
    void Start()
    {
        musicPlayer= GetComponent<AudioSource>();
        foreach (AudioSource aSources in SFX_AudioSources) {
            if (aSources == null)
                continue;
            aSources.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateAudioSources();
    }

    void updateAudioSources() {
        foreach (AudioSource aSources in SFX_AudioSources)
        {
            if (aSources == null)
                continue;
            Transform tf = aSources.transform;
            float dist = Vector3.Distance(transform.position, tf.position);
            if (dist <= hearingRange)
            {
                if (!aSources.enabled) {
                    aSources.enabled = true;
                }
                if (aSources.gameObject.tag == AudiologTag)
                {
                    updateAudioLog(aSources);
                    
                }
                else {
                    updateAudioSource(aSources);
                   
                }
                
            }
            else if (aSources.enabled) { 
                aSources.enabled = false;
            }   
        }
    }
    public void stopMusic() {
        musicPlayer.Stop();
    }
    
    public void playMusic(int index) {
        musicPlayer.clip = MusicTracks[index];
        musicPlayer.Play();
    }

    public void playMusicFadeIn(int index) {
        musicPlayer.volume = 0;
        playMusic(index);
        StartCoroutine(FadeIn());

    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public void stopMusicFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    /// <summary>
    /// Code routine that fade out music 
    /// Insprede by https://www.youtube.com/watch?v=qkhisBC1_zg
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    private IEnumerator FadeOut(float speed = 0.0005f)
    {
        while (musicPlayer.volume > 0)
        {
            musicPlayer.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
        musicPlayer.Stop();
    }
    /// <summary>
    /// Code routine that fade in music 
    /// Insprede by https://www.youtube.com/watch?v=qkhisBC1_zg
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    private IEnumerator FadeIn(float speed = 0.0005f)
    {
        while (musicPlayer.volume < 1)
        {
            musicPlayer.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void updateAudioLog(AudioSource aS) {
        AudioLogController alController = aS.gameObject.GetComponent<AudioLogController>();
        if (alController.isGrabbed) {
            if (!aS.enabled) { 
                aS.enabled = true;
            }
        }else
        {
            updateAudioSource(aS);
        }
    }
    void updateAudioSource(AudioSource aS) {
        // Get the direction the player is looking
        Vector3 lookDirection = transform.forward;

        // Calculate direction from player to this object
        Vector3 toObject = (aS.transform.position - transform.position).normalized;

        // Calculate angle between look direction and toObject direction
        float angle = Vector3.Angle(lookDirection, toObject);
        // Map the angle to the audio volume (angle 0 means maxVolume, angle >= angleToMaxVolume means volume 0)
        aS.volume = Mathf.Lerp(maxVolume, 0, angle / angleToMaxVolume);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = hearingGizmoColor;
        Gizmos.DrawSphere(transform.position, hearingRange);
    }

    public void playSFX(int index)
    {
        SFX_AudioSources[index].Play();
    }
    public void stopSFX(int index)
    {
        SFX_AudioSources[index].Stop();
    }
}
