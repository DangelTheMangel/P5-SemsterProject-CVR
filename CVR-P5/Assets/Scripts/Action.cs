using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Action : MonoBehaviour
{
    // Private field to store the default name for the action.
    [SerializeField]
    private string name = "_action";

    /// <summary>
    /// Virtual method for updating the action. Override this method in derived classes.
    /// </summary>
    public virtual void actionUpdate()
    {
        return;
    }

    /// <summary>
    /// Virtual method for starting the action. Override this method in derived classes.
    /// </summary>
    public virtual void startAction()
    {
        return;
    }

    /// <summary>
    /// Method to get the name of the action.
    /// </summary>
    /// <returns></returns>
    public string getActionName()
    {
        return name;
    }

    /// <summary>
    /// Virtual method for ending the action. Override this method in derived classes if needed.
    /// This method can be empty if ending the action doesn't require any specific logic.
    /// </summary>
    public virtual void endAction()
    {
    }
}
