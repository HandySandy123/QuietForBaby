using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamBehavior : MonoBehaviour
{
    PlayerInput inputActionAsset;
    InputAction moveCam;
    InputAction moveDown;
    private Vector3 firstFloorPos;
    private Vector3 secondFloorPos;
    private bool onFirstFloor = true;
    private bool onSecondFloor = false;
    private float duration = 10;

    [SerializeField] private GameObject[] firstFloor;
    [SerializeField] private float floorHeight;
    
    [SerializeField] private GameObject player;
    //private PlayerBehavior playerBehavior;
    [SerializeField] private float distanceFromPlayer = 10f;
    
    public List<Material> materials = new List<Material>();
    private float fadeFactor = 10f;
    private bool isMovingFloors = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActionAsset = GetComponent<PlayerInput>();
        Debug.Log(inputActionAsset.name);
        moveCam = inputActionAsset.actions.FindAction("MoveCam");
        firstFloorPos = transform.position;
        secondFloorPos = transform.position + Vector3.up * floorHeight;
        //playerBehavior = player.GetComponent<PlayerBehavior>();
        
        foreach (GameObject go in firstFloor)
        {
            foreach (var comp in go.GetComponent<Renderer>().materials)
            {
                if (!materials.Contains(comp))
                {
                    materials.Add(comp);
                }
            }
        }

        //transform.position = playerBehavior.camPos + new Vector3(distanceFromPlayer, 0, distanceFromPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
        moveCam.performed += ctx =>
        {
            changeFloors();
            isMovingFloors = true;
        };
        
        if (onFirstFloor && firstFloorPos != transform.position && isMovingFloors)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstFloorPos, duration * Time.deltaTime);
        } else if (onSecondFloor && secondFloorPos != transform.position && isMovingFloors)
        {
            transform.position = Vector3.MoveTowards(transform.position, secondFloorPos, duration * Time.deltaTime);
        } else if ((onFirstFloor && firstFloorPos == transform.position) ||
                   (onSecondFloor && secondFloorPos == transform.position))
        {
            isMovingFloors = false;
        }
    }

    void changeFloors()
    {
        
        if (onFirstFloor)
        {
            onFirstFloor = false;
            onSecondFloor = true;
            foreach (GameObject go in firstFloor)
            {
                go.GetComponent<Renderer>().enabled = true;
            }
            //StartCoroutine(FadeIn());
        }
        else
        {
            onFirstFloor = true;
            onSecondFloor = false;
            foreach (GameObject go in firstFloor)
            {
                go.GetComponent<Renderer>().enabled = false;
            }
            //StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        foreach (var mat in materials)
        {
            float time = 0;
            Color transparentColor = new Color(0, 0, 0, 1);
            
            
            while (time < fadeFactor)
            {
                mat.color = Color.Lerp(mat.color, transparentColor, time / fadeFactor);
                time += Time.deltaTime; 
                yield return null;
            }
            
            //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
        }
    }

    private IEnumerator FadeOut()
    {
        foreach (var mat in materials)
        {
            float time = 0;
            Color transparentColor = new Color(0, 0, 0, 0);
            
            
            while (time < fadeFactor)
            {
                mat.color = Color.Lerp(mat.color, transparentColor, time / fadeFactor);
                time += Time.deltaTime; 
                yield return null;
            }
            
            //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
        }
        
    }
}
