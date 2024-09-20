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
        movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        animator.SetFloat("Direction", Mathf.Abs(movement.magnitude * movementSpeed));

        bool flippedX = movement.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flippedX ? 180f : 0f, 0f));
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            var xMovement = movement.x * movementSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(xMovement, 0), Space.World);
        }
    }
}