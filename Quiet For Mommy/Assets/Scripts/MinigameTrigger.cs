using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    
    public bool ColOnToys = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is triggered by the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the first child object of the "Player+Camera" object
            GameObject childObject = other.gameObject.transform.GetChild(0).gameObject;


            // Activate the child object
            childObject.SetActive(true);
            Debug.Log("MinigameTrigger Object found!");

            ColOnToys = true;
        }else
        {
            // Get the first child object of the "Player+Camera" object
            GameObject childObject = other.gameObject.transform.GetChild(0).gameObject;

            // Deactivate the child object
            childObject.SetActive(false);

            ColOnToys = false;
        }
    }
}

