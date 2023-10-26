using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a list of actions and controls their execution.
/// </summary>
public class ActionManager : MonoBehaviour
{
    [Header("sequences")]
    [SerializeField]
    ActionSequence[] actionSequences;
    [SerializeField]
    int actionSequenceIndex = 0;
    [Header("actions")]

    [SerializeField]
    int currentActionIndex = -1; // Index of the currently active action.

    [SerializeField]
    bool actionsActive = true; // Flag indicating whether actions are active or paused.

    [Header("animation")]
    [SerializeField]
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// Update is called once per frame. Checks for active action and updates it.
    /// </summary>
    void Update()
    {
        if (currentActionIndex >= 0 && actionsActive)
        {
            getCurrentAction().actionUpdate();
        }
    }

    /// <summary>
    /// Ends the current action and starts a new one based on the provided action name.
    /// </summary>
    /// <param name="actionName">Name of the action to be started.</param>
    public void endAndStartNewActionWithSearch(string actionName)
    {
        for (int i = 0; i < getCurrentActionSequence().listOfActions.Length; i++)
        {
            if (actionName == getAction(i).getActionName())
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
        getCurrentAction().endAction();
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
        for (int i = 0; i < getCurrentActionSequence().listOfActions.Length; i++)
        {
            if (actionName == getAction(i).getActionName())
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
        getCurrentAction().startAction();
        enableAllActions();
        Debug.Log("starting action: " + getCurrentAction().name);
    }

    /// <summary>
    /// Runs the next action in the list.
    /// </summary>
    public void runNextAction()
    {
        if(currentActionIndex + 1 <= actionSequences[actionSequenceIndex].listOfActions.Length)
            runAction(currentActionIndex + 1);
    }

    public Action getAction(int index) {
        return actionSequences[actionSequenceIndex].listOfActions[index];
    }

    public Action getCurrentAction() {
        return getAction(currentActionIndex);
    }

    public ActionSequence getCurrentActionSequence()
    {
        return getActionSequence(currentActionIndex);
    }

    public ActionSequence getActionSequence(int index)
    {
       return actionSequences[index];
    }

    public void runNextActionSequence() {
        if (actionSequenceIndex + 1 <= actionSequences.Length) {
            runActionSequence(actionSequenceIndex + 1);
        }
    }

    public void runActionSequence(int index) {
        ActionSequence aS = actionSequences[index];
        actionSequenceIndex = index;
        animator.Play(aS.AnimationName);
    }
}
