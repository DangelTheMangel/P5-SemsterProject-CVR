using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOpenCanvasItem : Action
{
    public GameObject canvasItem;
    public override void startAction()
    {
        canvasItem.SetActive(true);
    }
}
