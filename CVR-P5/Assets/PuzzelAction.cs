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
    int code = 121;
    [SerializeField]
    int max = 3, min = 1;
    [SerializeField]
    public bool gotCode = false;
    public void pressButton(int index) {
        if (gotCode)
            return;
        digigts[index]++;
        if (digigts[index] < max) {
            digigts[index] = min;
        }
        screenDigigts[index].text = digigts[index].ToString();
        string currentCode = digigts[0].ToString() + digigts[0].ToString() + digigts[0].ToString();
        if (currentCode == code.ToString()) {
            gotCode = true;
        }
    }
    


}
