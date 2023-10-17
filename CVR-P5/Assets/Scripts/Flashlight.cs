using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    // Dette script behøver ikke være for sig selv, vi kan merge den med en overordnet player controller senere.

    [SerializeField] private GameObject flashlightSwitch;
    public InputActionProperty flashlightButton;
    private bool flashlightActive = false;
    
    
    void Start()
    {
        flashlightSwitch.gameObject.SetActive(false);
    }


    void Update()
    {
        float flashValue = flashlightButton.action.ReadValue<float>();
        if (flashValue == 1)
        {
            if (flashlightActive == false)
            {
                flashlightSwitch.gameObject.SetActive(true);
                flashlightActive = true;
            }
            else
            {
                flashlightSwitch.gameObject.SetActive(false);
                flashlightActive = false;
            }
        }
    }
}
