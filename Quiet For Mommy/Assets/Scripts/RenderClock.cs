using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderClock : MonoBehaviour
{
    public float radius = 1f;
    public float startAngle;        // In degrees
    public float endAngle;         // In degrees
    public int segments = 10;
    private MeshFilter mf;
    
    [SerializeField] private GameObject clock;
    TimerClock timerClock;
    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        timerClock = clock.GetComponent<TimerClock>();
        //radius = GetComponentInParent<Renderer>().bounds.extents.magnitude;

        // Start at the correct space
        startAngle = -timerClock.correctSpace;
        endAngle = timerClock.correctSpace;

        // Generate the mesh
        mf.mesh = GenerateSectorMesh(radius, startAngle, endAngle, segments);
        
        // Rotate the mesh so it's on its side
        mf.transform.Rotate(0, -90, 90);
        // Move the mesh so it's centered
        Vector3 newPosition = mf.transform.position;
        newPosition.z -= 0.001f;
        mf.transform.position = newPosition;
        //transform.Rotate(new Vector3(0, 0, 1), endAngle * 0.5f, Space.World);
    }

    Mesh GenerateSectorMesh(float radius, float startAngleDeg, float endAngleDeg, int segments)
    {
        Mesh mesh = new Mesh();
        int vertexCount = segments + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[segments * 3];
        float startRad = Mathf.Deg2Rad * startAngleDeg;
        float endRad = Mathf.Deg2Rad * endAngleDeg;
        float angleStep = (endRad - startRad) / segments;

        // Center vertex
        vertices[0] = Vector3.zero;

        // Generate the vertices
        for (int i = 0; i <= segments; i++)
        {
            float angle = startRad + i * angleStep;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            vertices[i + 1] = new Vector3(x, 0, z);
        }

        // Generate the triangles
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;           // center
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        // Set the vertices and triangles
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
    void Update()
    {
        mf = GetComponent<MeshFilter>();
        // Get the position of the "pointerEnd" object
        Transform pointerEnd = GameObject.Find("pointerEnd").transform;

        // Create a ray from the pointerEnd position, along the Z axis
        Ray ray = new Ray(pointerEnd.position, pointerEnd.forward);

        // Perform the raycast
        RaycastHit hit;

        Debug.DrawRay(pointerEnd.position, pointerEnd.forward * 2f, Color.red);

        if (mf != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Color targetColor = new Color32(0x1A, 0xFF, 0x00, 0x1A);

                if (Physics.Raycast(ray, out hit))
                {
                    Renderer renderer = hit.transform.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Material material = renderer.material;
                        Color color = material.color;
                        Debug.Log("Hit color: " + color);
                        // Check the color
                        if (color == targetColor)
                        {
                            // Do something
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("MeshFilter is null!");
        }
    }
}
