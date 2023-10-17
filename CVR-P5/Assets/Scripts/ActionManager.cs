using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [Header("actions")]
    [SerializeField]
    Action[] listOfActions;
    [SerializeField]
    int currentActionIndex = -1;
    [SerializeField]
    bool actionsActive = true;

    // Update is called once per frame
    void Update()
    {
        if (currentActionIndex >= 0 && actionsActive) {
            listOfActions[currentActionIndex].actionUpdate();
        }
    }

    public void endAndStartNewActionWithSearch(string actionName)
    {
        for (int i = 0; i < listOfActions.Length; i++)
        {
            if (actionName == listOfActions[i].getActionName())
            {
                endAndStartNewAction(i);
                break;
            }
        }
    }
    public void endAndStartNewAction(int actionIndex) {
        endAction();
        runAction(actionIndex);
    }

    public void endAction()
    {
        listOfActions[currentActionIndex].endAction();
        disableAllActions();
    }

    public void disableAllActions() {
        actionsActive = false;
    }
    public void enableAllActions()
    {
        actionsActive = true;
    }
    public void runActionWithSearch(string actionName) {
        for (int i = 0; i < listOfActions.Length; i++) {
            if (actionName == listOfActions[i].getActionName()) {
                runAction(i);
                break;
            }
        }
    }

    public void runAction(int actionIndex)
    {
        currentActionIndex = actionIndex;
        listOfActions[currentActionIndex].startAction();
        enableAllActions();
    }

    public void runNextAction() {
        runAction(currentActionIndex+1);
    }
}
