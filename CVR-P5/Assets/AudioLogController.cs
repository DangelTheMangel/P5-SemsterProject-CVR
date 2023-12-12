using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class AudioLogController : MonoBehaviour
{
    AudioSource audioSource;
    public bool isGrabbed = false;
    public bool canPress = false;
    float canpressTimer = 1f;
    [SerializeField]
    AudioClip audiolog;
    AudioClip idealSound;
    [SerializeField]
    bool isplaying = false;
    AudioLowPassFilter lowpassfilter;
    AudioHighPassFilter highPassFilter;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable xrGrab = GetComponent<XRGrabInteractable>();
        xrGrab.activated.AddListener(clickOnAL);
        audioSource = GetComponent<AudioSource>();
        idealSound = audioSource.clip;
        lowpassfilter= GetComponent<AudioLowPassFilter>();
        highPassFilter= GetComponent<AudioHighPassFilter>();
    }

    void updateFilters(bool enable) {
        if (lowpassfilter != null) {
            lowpassfilter.enabled = enable;
        }

        if (highPassFilter != null) {
            highPassFilter.enabled = enable;
        }
    
    }

    void clickOnAL(ActivateEventArgs activateEventArgs) {
        if (canPress) { 
            if (isplaying)
            {
                audioSource.clip = idealSound;
                audioSource.Play();
                isplaying = false;
                updateFilters(true);
            }
            else {
                audioSource.clip = audiolog;
                audioSource.Play();
                isplaying = true;
                updateFilters(false);
            }
        }
        
    }
    public void focused() {
        canPress = true;
    }

    public void notFocused() {
        canPress = false;

    }
    public void grabbed() {
        isGrabbed = !isGrabbed;
    }

}
