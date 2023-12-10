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
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable xrGrab = GetComponent<XRGrabInteractable>();
        xrGrab.activated.AddListener(clickOnAL);
        audioSource = GetComponent<AudioSource>();
        idealSound = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickOnAL(ActivateEventArgs activateEventArgs) {
        if (canPress) { 
            if (isplaying)
            {
                audioSource.clip = idealSound;
                audioSource.Play();
                isplaying = false;
            }
            else {
                audioSource.clip = audiolog;
                audioSource.Play();
                isplaying = true;
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
