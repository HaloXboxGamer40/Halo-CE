using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject objectToAppear; // Assign in Inspector
    public bool hasRan = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasRan) // Check if the player collides
        {
            objectToAppear.SetActive(true); // Make the object appear
        }
    }
}
