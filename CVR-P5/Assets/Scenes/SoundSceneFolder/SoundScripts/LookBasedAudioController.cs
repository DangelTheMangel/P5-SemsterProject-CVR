using UnityEngine;

// Define your class
public class LookBasedAudioController : MonoBehaviour
{
    public Transform playerHead; // Assign your VR camera transform here
    public AudioSource audioSource; // Assign the object's audio source here
    public float maxVolume = 1.0f; // The maximum volume the audio source should reach
    public float angleToMaxVolume = 15f; // Angle at which the audio will play at maxVolume

    // Use the Update method, which is called once per frame
    void Update()
    {
        // Get the direction the player is looking
        Vector3 lookDirection = playerHead.forward;

        // Calculate direction from player to this object
        Vector3 toObject = (transform.position - playerHead.position).normalized;

        // Calculate angle between look direction and toObject direction
        float angle = Vector3.Angle(lookDirection, toObject);

        // Map the angle to the audio volume (angle 0 means maxVolume, angle >= angleToMaxVolume means volume 0)
        audioSource.volume = Mathf.Lerp(maxVolume, 0, angle / angleToMaxVolume);

        // Ensure the audio plays if it's not already doing so
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
