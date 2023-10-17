using System;
using UnityEngine;

[Serializable]
public class SineWaveMoveActionTest : Action
{
    public float amplitude = 1f;
    public float frequency = 1f;
    public float speed = 1f;

    private Vector3 initialPosition;

    public override void startAction()
    {
        base.startAction();
        initialPosition = transform.position;
    }

    public override void actionUpdate()
    {
        base.actionUpdate();

        float yOffset = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        Vector3 newPosition = initialPosition + new Vector3(0f, yOffset, 0f);
        transform.position = newPosition;
    }
}
