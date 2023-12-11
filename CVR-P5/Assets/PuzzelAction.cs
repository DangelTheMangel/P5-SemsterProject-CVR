using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzelAction : MonoBehaviour
{
    [SerializeField]
    GameObject codePart, finnishPar; 
    [SerializeField]
    int[] digigts = { 0,0,0};
    [SerializeField]
    TMP_Text[] screenDigigts;
    [SerializeField]
    int code = 123;
    [SerializeField]
    int max = 3, min = 1;
    [SerializeField]
    public bool gotCode = false;
    bool[] ispressed = { false, false, false };
    [SerializeField]
    CapsuleCollider capsuleCollider;
    [SerializeField]
    GameObject[] iceParts;
    [SerializeField]
    XRGrabInteractable grab;
    public void pressButton(int index) {
        ispressed[index] = true;
    }

    public void exitPressButton(int index)
    {
        if (gotCode)
        {
            return;
        }
        else if (ispressed[index]) { 
             digigts[index] += 1;
            if (digigts[index] > max)
            {
                digigts[index] = min;
            }
            screenDigigts[index].text = digigts[index].ToString();
            string currentCode = digigts[0].ToString() + digigts[1].ToString() + digigts[2].ToString();
            Debug.Log(currentCode + " == " + code.ToString() + "[" + (currentCode == code.ToString()) + "]");
            if (currentCode == code.ToString())
            {
                gotCode = true;
                codePart.SetActive(false);
                finnishPar.SetActive(true);
                activate();
            }
            ispressed[index] = false;
        }

       
    }

    public void activate() {
        capsuleCollider.enabled = true;
        grab.enabled = true;
        for (int i = 0; i < iceParts.Length; i++) {
            iceParts[i].SetActive(false);
        }
    }

}
