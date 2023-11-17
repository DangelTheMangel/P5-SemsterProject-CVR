using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Inspriede by : https://youtu.be/bts8VkDP_vU?si=wKe3-S7T5JHErEWw
/// </summary>
public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5;
    public float followAngleThreshold = 45;

    [SerializeField]
    private bool freeze = false;
    [SerializeField]
    private Vector3 initialLocalPos;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Transform pokeAttachTransform;
    [SerializeField]
    private XRBaseInteractable interactable;
    [SerializeField]
    private bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;

            isFollowing = true;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if(pokeAngle > followAngleThreshold)
            {
                isFollowing = false;
                freeze = true;
            }
        }
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(freeze)
            return;

        if(isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}