using System.Collections;
using UnityEngine;
/// <summary>
/// inspired by this project: https://github.com/joeythelantern/AsteroidBeltExample/tree/master/Assets
/// </summary>
public class OrbitingObject : MonoBehaviour
{
    [SerializeField]
    private float orbitSpeed; // Speed at which the object orbits around its parent.
    [SerializeField]
    private GameObject parentObject; // The object around which this object orbits.
    [SerializeField]
    private bool rotateClockwise; // Determines the direction of orbit.
    [SerializeField]
    private float rotationSpeed; // Speed at which the object rotates around itself.
    [SerializeField]
    private Vector3 randomRotationAxis; // Random rotation axis for variation.

    /// <summary>
    /// Set up the orbiting object with specified parameters.
    /// </summary>
    public void SetupOrbitingObject(float speed, float rotationSpeed, GameObject parent, bool clockwise)
    {
        orbitSpeed = speed;
        this.rotationSpeed = rotationSpeed;
        parentObject = parent;
        rotateClockwise = clockwise;
        randomRotationAxis = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    private void Update()
    {
        // Orbit around the parent object.
        Vector3 orbitDirection = rotateClockwise ? parentObject.transform.up : -parentObject.transform.up;
        transform.RotateAround(parentObject.transform.position, orbitDirection, orbitSpeed * Time.deltaTime);

        // Rotate around own axis.
        transform.Rotate(randomRotationAxis, rotationSpeed * Time.deltaTime);
    }
}
