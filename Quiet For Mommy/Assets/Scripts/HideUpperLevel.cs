using UnityEngine;

public class HideUpperLevel : MonoBehaviour{
    public bool isUpperFloorActive = false;
    
    void OnTriggerEnter(Collider other) {
        isUpperFloorActive = true;
        Debug.Log("Object entered trigger area");

    }

    void OnTriggerExit(Collider other)
    {
        isUpperFloorActive = false;
        Debug.Log("Object exited trigger area");
        transform.parent.gameObject.SetActive(false);
    }
}