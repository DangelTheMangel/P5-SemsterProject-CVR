using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    public GameObject pointLight1, pointlight2;
    public void turnOnAndOff() {
        bool turn = !pointlight2.active;
        pointLight1.SetActive(turn);
        pointlight2.SetActive(turn);
    }
}
