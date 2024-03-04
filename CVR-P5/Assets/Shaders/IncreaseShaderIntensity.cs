using UnityEngine;

public class IncreaseShaderIntensity : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public string blackHoleTag = "BlackHole"; // Tag of the black hole object
    public Material shaderMaterial; // Reference to the material with the shader

    private float maxDistance = 10f; // Maximum distance for full intensity
    private float minDistance = 2f; // Minimum distance for no intensity change

    private void Update()
    {
        // Find the black hole object with the specified tag
        GameObject blackHole = GameObject.FindGameObjectWithTag(blackHoleTag);

        if (blackHole != null)
        {
            // Calculate the distance to the black hole
            float distance = Vector3.Distance(player.position, blackHole.transform.position);

            // Calculate the intensity based on the distance
            float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
            float intensity = 1f - normalizedDistance; // Invert the intensity

            // Set the shader intensity parameter
            shaderMaterial.SetFloat("GlitchSpeed", intensity);
        }
        else
        {
            Debug.LogWarning($"No object with tag '{blackHoleTag}' found.");
        }
    }
}   

