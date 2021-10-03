using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollow : MonoBehaviour
{
    public Camera camera;
    public PlayerScript player;

    private void Awake()
    {
        
    }
    private void Update()
    {
        Vector3 newPos = player.transform.position;
        newPos.z = -10f;
        camera.transform.position = newPos;

    }
}
