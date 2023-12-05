using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HandAnimationController : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty gripAnimation;

    public Animator handAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pinchValue = pinchAnimation.action.ReadValue<float>();
        float gripValue = gripAnimation.action.ReadValue<float>();
        pinchValue = Mathf.Clamp01(pinchValue);
        gripValue = Mathf.Clamp01(gripValue);
        handAnimation.SetFloat("Grip", gripValue);
        handAnimation.SetFloat("Pinch", pinchValue);
    }
}
