using UnityEngine;

public class HideUpperLevel : MonoBehaviour{
    public bool isUpperFloorActive = false;
    

    void OnTriggerExit(Collider other)
    {
        if (!isUpperFloorActive)
        {
            isUpperFloorActive = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            isUpperFloorActive = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}