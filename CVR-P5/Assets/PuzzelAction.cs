using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public void pressButton(int index) {
        if (gotCode) {
            return;
        }
            
        digigts[index] += 1;
        if (digigts[index] > max) {
            digigts[index] = min;
        }
        screenDigigts[index].text = digigts[index].ToString();
        string currentCode = digigts[0].ToString() + digigts[1].ToString() + digigts[2].ToString();
        Debug.Log(currentCode  + " == " + code.ToString() + "["+(currentCode == code.ToString()) +"]");
        if (currentCode == code.ToString()) {
            gotCode = true;
            codePart.SetActive(false);
            finnishPar.SetActive(true);
        }
    }
    


}
