using UnityEngine;

public class CreakyFloorboards : MonoBehaviour
{
    public AudioSource source;
    BoxCollider soundTrigger;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<BoxCollider>();
    }

   public void OnTriggerEnter(Collider other)
    {
        source.Play();
    }
    
        
}


