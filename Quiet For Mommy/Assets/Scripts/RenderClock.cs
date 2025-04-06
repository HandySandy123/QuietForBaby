using System.Collections.Generic;
using UnityEngine;

public class RenderClock : MonoBehaviour
{
    public float radius = 1f;
    public float startAngle;        // In degrees
    public float endAngle;         // In degrees
    public int segments = 10;
    
    [SerializeField] private GameObject clock;
    TimerClock timerClock;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        timerClock = clock.GetComponent<TimerClock>();
        //radius = GetComponentInParent<Renderer>().bounds.extents.magnitude;

        startAngle = -timerClock.correctSpace;
        endAngle = timerClock.correctSpace;
        
        mf.mesh = GenerateSectorMesh(radius, startAngle, endAngle, segments);
        
        
        mf.transform.Rotate(0, -90, 90);
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

        vertices[0] = Vector3.zero; // Center vertex

        for (int i = 0; i <= segments; i++)
        {
            float angle = startRad + i * angleStep;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            vertices[i + 1] = new Vector3(x, 0, z);
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;           // center
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
