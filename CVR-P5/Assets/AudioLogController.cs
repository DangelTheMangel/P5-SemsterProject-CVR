using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class AudioLogController : MonoBehaviour
{
    AudioSource audioSource;
    public bool isGrabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable xrGrab = GetComponent<XRGrabInteractable>();
        xrGrab.activated.AddListener(clickOnAL);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickOnAL(ActivateEventArgs activateEventArgs) {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else {
            audioSource.Play();
        }
    }

    public void grabbed() {
        isGrabbed = !isGrabbed;
    }
}
