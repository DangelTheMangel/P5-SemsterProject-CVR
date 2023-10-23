using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// inspired by this project: https://github.com/joeythelantern/AsteroidBeltExample/tree/master/Assets
/// </summary>
public class BeltGenerator : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject cubePrefab;
    public int density;
    public int seed;
    public float innerRadius;
    public float outerRadius;
    public float height;
    public bool isRotatingClockwise;

    [Header("Asteroid Settings")]
    public float minOrbitSpeed;
    public float maxOrbitSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;

    private Vector3 localPos;
    private Vector3 worldOffset;
    private Vector3 worldPos;
    private float randomRadius;
    private float randomRadian;
    private float posX;
    private float posY;
    private float posZ;

    private void Start()
    {
        // Initialize the random number generator with the provided seed
        Random.InitState(seed);

        // Loop to spawn asteroids based on density
        for (int i = 0; i < density; i++)
        {
            // Generate random polar coordinates within specified inner and outer radii
            do
            {
                randomRadius = Random.Range(innerRadius, outerRadius);
                randomRadian = Random.Range(0, (2 * Mathf.PI));

                posY = Random.Range(-(height / 2), (height / 2));
                posX = randomRadius * Mathf.Cos(randomRadian);
                posZ = randomRadius * Mathf.Sin(randomRadian);
            }
            while (float.IsNaN(posZ) && float.IsNaN(posX));

            // Calculate local and world positions for the asteroid
            localPos = new Vector3(posX, posY, posZ);
            worldOffset = transform.rotation * localPos;
            worldPos = transform.position + worldOffset;

            // Instantiate an asteroid prefab with random rotation and add BeltObject component to it
            GameObject asteroid = Instantiate(cubePrefab, worldPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            asteroid.AddComponent<OrbitingObject>().SetupOrbitingObject(Random.Range(minOrbitSpeed, maxOrbitSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), gameObject, isRotatingClockwise);
            asteroid.transform.SetParent(transform);
        }
    }
}
