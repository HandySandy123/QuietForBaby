using UnityEngine;

public class PickupObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PickUpLayer"))
        {
            Debug.Log("Collision PickUpLayer");
        }
    }
}