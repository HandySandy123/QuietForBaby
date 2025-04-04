using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamBehavior : MonoBehaviour
{
    PlayerInput inputActionAsset;
    InputAction moveCam;
    InputAction moveDown;
    private Vector3 firstFloor;
    private Vector3 secondFloor;
    private bool onFirstFloor = true;
    private bool onSecondFloor = false;
    private float duration = 1;
    
    [SerializeField] private float floorHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActionAsset = GetComponent<PlayerInput>();
        Debug.Log(inputActionAsset.name);
        moveCam = inputActionAsset.actions.FindAction("MoveCam");
        firstFloor = transform.position;
        secondFloor = transform.position + Vector3.up * floorHeight;
    }

    // Update is called once per frame
    void Update()
    {
        moveCam.performed += ctx => StartCoroutine(MoveCam());
    }

    IEnumerator MoveCam()
    {
        if (onFirstFloor)
        {
            //transform.position = secondFloor * new Vector3(0, Time.deltaTime, 0);
            while (secondFloor != transform.position)
            {
                Debug.Log("Moving Up");
                transform.position = Vector3.Lerp(transform.position, secondFloor, Time.deltaTime * duration);
            }
            
        }
        else
        {
            while (firstFloor != transform.position)
            {
                Debug.Log("Moving Down");
                transform.position = Vector3.Lerp(transform.position, secondFloor, Time.deltaTime * duration);
            }
            transform.position = firstFloor;
        }
        onFirstFloor = !onFirstFloor;
        onSecondFloor = !onSecondFloor;
        yield return null;
    }
}
