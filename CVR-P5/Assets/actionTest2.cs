using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionTest2 : Action
{
    public Color targetColor;
    public float duration = 1f;

    private Color initialColor;
    private float startTime;

    public override void startAction()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            initialColor = renderer.material.color;
            startTime = Time.time;
        }
    }

    public override void actionUpdate()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            renderer.material.color = Color.Lerp(initialColor, targetColor, t);

            if (t >= 1f)
            {
                endAction();
            }
        }
    }
}
