using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("EndningChecks")]
    [SerializeField]
    GameObject alienArtifact,BlackHole;
    [SerializeField]
    float distanceToHole = 100;
    bool experinceStarted = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        runActionSequence(actionSequenceIndex);
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

    public void startExperinces() {
        if (!experinceStarted) {
            experinceStarted = true;
            runNextActionSequence();
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
            Debug.Log("The next sequnce are:  " + (actionSequenceIndex + 1));
            runActionSequence(actionSequenceIndex + 1);
        }
    }

    public void runActionSequence(int i) {
        ActionSequence aS = actionSequences[i];
        actionSequenceIndex = i;
        animator.Play(aS.AnimationName);
        Debug.Log("Playing Action sequnce:  " + i);
    }

    public void restartScene() {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (alienArtifact != null && BlackHole != null) {
            if (Vector3.Distance(alienArtifact.transform.position, BlackHole.transform.position) <= distanceToHole) {
                index += 1;
            }
        }
        SceneManager.LoadScene(index);
    }
}
