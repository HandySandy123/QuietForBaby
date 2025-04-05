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
    private float duration = 10;
    
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
        moveCam.performed += ctx => MoveCam();
        if (onFirstFloor && firstFloor != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstFloor, duration * Time.deltaTime);
        } else if (onSecondFloor && secondFloor != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, secondFloor, duration * Time.deltaTime);
        }
    }

    void MoveCam()
    {
        if (onFirstFloor)
        {
            onFirstFloor = false;
            onSecondFloor = true;
        }
        else
        {
            onFirstFloor = true;
            onSecondFloor = false;
        }
        
    }
}
