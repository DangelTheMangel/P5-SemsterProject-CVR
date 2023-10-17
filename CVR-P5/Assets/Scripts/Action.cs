using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Action: MonoBehaviour
{
    [SerializeField]
    private string name = "_action";

    public virtual void actionUpdate() {
        return;
    }

    public virtual void startAction()
    {
        return;
    }

    public string getActionName() {
        return name;
    }
    public virtual void endAction() {
    
    }
}
