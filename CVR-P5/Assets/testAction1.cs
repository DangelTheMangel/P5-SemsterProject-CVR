using System;
using UnityEngine;

[Serializable]
public class testAction1 : Action
{
    [SerializeField]
    GameObject affectedGameObject;
    public override void actionUpdate() {
        affectedGameObject.transform.position += new Vector3(UnityEngine.Random.Range(2, 2), UnityEngine.Random.Range(2, 2), UnityEngine.Random.Range(2, 2));
    }
}
