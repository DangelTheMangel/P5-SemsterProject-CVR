using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienSwarmController : MonoBehaviour
{
    [SerializeField]
    List<Alien> aliens = new List<Alien>();
    [SerializeField]
    GameObject alienPrefab;
    [SerializeField]
    Transform walkingPostion;
    [SerializeField]
    int amount = 20;

    [SerializeField]
    SwamPostion[] swamPostions;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++) {
            int index = UnityEngine.Random.Range(0, swamPostions.Length);
            SwamPostion sp = swamPostions[index];
            Vector3 pos = sp.randomPostionInsideArea();
            GameObject obj = Instantiate(alienPrefab, pos, Quaternion.identity);
            Alien alien = new Alien(obj.GetComponent<NavMeshAgent>(), sp.swamble);
            alien.meshAgent.SetDestination(sp.randomPostionInsideArea());
            alien.index= index;
            aliens.Add(alien);

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Alien alien in aliens)
        {
            alien.faceMovement();
            if (alien.newPostionNeed()) {
                if (alien.swarming)
                {
                    alien.meshAgent.SetDestination(swamPostions[alien.index].randomPostionInsideArea());
                }
                else {
                    alien.index += alien.addOn;
                    if (alien.index >= swamPostions.Length || alien.index < 0) {
                        alien.addOn *= -1;
                        alien.index += alien.addOn;
                        alien.meshAgent.SetDestination(swamPostions[alien.index].randomPostionInsideArea());
                    }
                    else
                    {
                        alien.meshAgent.SetDestination(swamPostions[alien.index].randomPostionInsideArea());
                    }
               
                }
            }
        }
    }
}
[Serializable]
public class SwamPostion
{
    public Transform transform;
    public bool swamble;
    public BoxCollider boxOFSwarm;
    SwamPostion(Transform transform, bool swamble)
    {
        this.transform = transform;
        this.swamble = swamble;
    }
    public Vector3 randomPostionInsideArea()
    {
       
        return new Vector3(
            UnityEngine.Random.Range(boxOFSwarm.bounds.min.x, boxOFSwarm.bounds.max.x),
            UnityEngine.Random.Range(boxOFSwarm.bounds.min.y, boxOFSwarm.bounds.max.y),
            UnityEngine.Random.Range(boxOFSwarm.bounds.min.z, boxOFSwarm.bounds.max.z)
        );
    }
}
[Serializable]
public class Alien{
    public NavMeshAgent meshAgent;
    public int index = 0;
    public int addOn = 1;
    public bool swarming = false;
    public Transform transform;
    [SerializeField]
    float distanceForNextPoint = 0.3f;
    public Alien(NavMeshAgent navMeshAgent, bool swarmingTheArea) {
        meshAgent = navMeshAgent;
        swarming = swarmingTheArea;
        transform = meshAgent.gameObject.transform;
    }
    public bool newPostionNeed() {
        return (meshAgent.remainingDistance <= distanceForNextPoint);
    }

    //inspried by https://forum.unity.com/threads/how-do-i-update-the-rotation-of-a-navmeshagent.707579/
    public void faceMovement()
    {
        var turnTowardNavSteeringTarget = meshAgent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

}
