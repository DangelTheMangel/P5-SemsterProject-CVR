using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a list of actions and controls their execution.
/// </summary>
public class ActionManager : MonoBehaviour
{
    [Header("actions")]
    [SerializeField]
    Action[] listOfActions; // Array to store different Action instances.

    [SerializeField]
    int currentActionIndex = -1; // Index of the currently active action.

    [SerializeField]
    bool actionsActive = true; // Flag indicating whether actions are active or paused.

    /// <summary>
    /// Update is called once per frame. Checks for active action and updates it.
    /// </summary>
    void Update()
    {
        if (currentActionIndex >= 0 && actionsActive)
        {
            listOfActions[currentActionIndex].actionUpdate();
        }
    }

    /// <summary>
    /// Ends the current action and starts a new one based on the provided action name.
    /// </summary>
    /// <param name="actionName">Name of the action to be started.</param>
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

    /// <summary>
    /// Ends the current action and starts a new one based on the provided index.
    /// </summary>
    /// <param name="actionIndex">Index of the action to be started.</param>
    public void endAndStartNewAction(int actionIndex)
    {
        endAction();
        runAction(actionIndex);
    }

    /// <summary>
    /// Ends the current action and disables all actions.
    /// </summary>
    public void endAction()
    {
        listOfActions[currentActionIndex].endAction();
        disableAllActions();
    }

    /// <summary>
    /// Disables all actions.
    /// </summary>
    public void disableAllActions()
    {
        actionsActive = false;
    }

    /// <summary>
    /// Enables all actions.
    /// </summary>
    public void enableAllActions()
    {
        actionsActive = true;
    }

    /// <summary>
    /// Runs the action with the provided name.
    /// </summary>
    /// <param name="actionName">Name of the action to be executed.</param>
    public void runActionWithSearch(string actionName)
    {
        for (int i = 0; i < listOfActions.Length; i++)
        {
            if (actionName == listOfActions[i].getActionName())
            {
                runAction(i);
                break;
            }
        }
    }

    /// <summary>
    /// Runs the action with the provided index.
    /// </summary>
    /// <param name="actionIndex">Index of the action to be executed.</param>
    public void runAction(int actionIndex)
    {
        currentActionIndex = actionIndex;
        listOfActions[currentActionIndex].startAction();
        enableAllActions();
    }

    /// <summary>
    /// Runs the next action in the list.
    /// </summary>
    public void runNextAction()
    {
        runAction(currentActionIndex + 1);
    }
}
