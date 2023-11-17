using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ClickTest : MonoBehaviour
{
    XRPokeFollowAffordance pokeFollowAffordance;

    private void Start()
    {
        pokeFollowAffordance = GetComponent<XRPokeFollowAffordance>();
       
    }
    public void click() {
        Debug.Log("click");
    }
}
