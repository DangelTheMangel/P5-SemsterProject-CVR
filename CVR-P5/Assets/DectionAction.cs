using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectionAction : MonoBehaviour
{
    public ActionManager actionManager;
    public void clickStartNextAS(int i) {
        Debug.Log("Action sequnce selected is:" + i );
        actionManager.runActionSequence(i);
    }
}
