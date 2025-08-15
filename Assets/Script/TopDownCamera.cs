using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    private Camera targetCamera;
    [SerializeField] private float margin = 1f; 
    
    void Start()
    {
        targetCamera = Camera.main;
    }

    public void PositionCamera(int width, int height,float cellSize)
    {
        float mazeWidth = width*cellSize;
        float mazeHeight = height*cellSize;

        Vector3 center = new Vector3(mazeWidth / 2f - cellSize/2f, 0, mazeHeight / 2f - cellSize/2f);

        targetCamera.transform.position = center;
        targetCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        
        float aspect = targetCamera.aspect;
        float sizeX = (mazeWidth / 2f) / aspect;
        float sizeZ = mazeHeight / 2f;

        float orthoSize = Mathf.Max(sizeX, sizeZ) + margin;
        targetCamera.orthographic = true;
        targetCamera.orthographicSize = orthoSize;
        
        targetCamera.transform.position = new Vector3(center.x, 10f, center.z);
    }
}
