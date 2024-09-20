using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 4;
    private Vector2 movement;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        animator.SetFloat("Horizontal", Mathf.Abs(movement.x * movementSpeed));
        animator.SetFloat("Vertical", Mathf.Abs(movement.y * movementSpeed));
        
        // Calculate angle
        float angleY = 0f;
        float angleZ = 0f;

        if (movement.x < 0)
        {
            angleY = 180f;
        }
        else if (movement.x > 0)
        {
            angleY = 0f;
        }
        
        if (movement.y < 0 && movement.x != 0)
        {
            angleZ = -15f;
        }
        else if (movement.y > 0 && movement.x != 0)
        {
            angleZ = 15f;
        }
        
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, angleY, angleZ));
    }

    void FixedUpdate()
    {
        if (movement.x != 0)
        {
            var xMovement = movement.x * movementSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(xMovement, 0), Space.World);
        }
        
        if (movement.y != 0)
        {
            var yMovement = movement.y * movementSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(0, yMovement), Space.World);
        }
    }
}