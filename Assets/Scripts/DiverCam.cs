using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private float xWidthC = 1.2f;
    private float yHeightC = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -xWidthC, xWidthC),
            Mathf.Clamp(player.position.y, -yHeightC, yHeightC), transform.position.z);
    }
}