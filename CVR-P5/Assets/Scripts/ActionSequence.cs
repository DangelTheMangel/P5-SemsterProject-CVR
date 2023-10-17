using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionSequence
{
    [SerializeField]
    public string name = "_actionSequences"; 
    [SerializeField]
    public Action[] listOfActions; // Array to store different Action instances.
    [SerializeField]
    public string AnimationName = "";
}
