using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float fallSpeed = 0;
    [SerializeField] private float fallAcceleration;
    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool doubleJump = true;
    [SerializeField] private float jumpSpeed;


    [SerializeField] private Vector3 moveDirection;
    private CharacterController characterController;
    private LayerMask layerMask = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float XDirection = Input.GetAxis("Horizontal");
        
        currentSpeed += XDirection * acceleration * Time.deltaTime;

        if (currentSpeed > maxSpeed) { currentSpeed = maxSpeed; }
        else if (currentSpeed < -maxSpeed) { currentSpeed = -maxSpeed; }

        if (XDirection == 0 && currentSpeed > 0) { currentSpeed -= deceleration * Time.deltaTime; }
        else if (XDirection == 0 && currentSpeed < 0) { currentSpeed += deceleration * Time.deltaTime; }

        if (XDirection == 0 && Mathf.Abs(currentSpeed) < 0.1) { currentSpeed = 0; }


        if (characterController.isGrounded && isJumping == false)
        {
            //Debug.Log("grounded");
            fallSpeed = -0.1f;
            jumpEnabled = true;
        }
        else
        {
            //Debug.Log("not grounded");
            if (fallSpeed >= maxFallSpeed)
            {
                fallSpeed += fallAcceleration * Time.deltaTime;
            }
        }

        /*
        if (!characterController.isGrounded)
        {
            RaycastHit Ground;
            RaycastHit InFront;
            RaycastHit Behind;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.3f, Color.yellow);           
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out Ground, 1.3f))
            {
                Debug.Log("raycast hit");
                jumpEnabled = true;
            }
            else
            {
                Debug.DrawRay(transform.position, new Vector3(.5f, -1.3f, 0) * 1, Color.yellow);
                if (Physics.Raycast(transform.position, new Vector3(-.5f, -1.3f, 0), out Ground, 1))
                {
                    jumpEnabled = true;

                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1f, Color.red);
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Behind, 1))
                    {
                        if (Ground.Equals(Behind))
                        {
                            jumpEnabled = false;
                        }
                    }
                }

                Debug.DrawRay(transform.position, new Vector3(-.5f, -1.3f, 0) * 1, Color.yellow);
                if (Physics.Raycast(transform.position, new Vector3(.5f, -1.3f, 0), out Ground, 1))
                {
                    jumpEnabled = true;

                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1f, Color.red);
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1f, out InFront, 1))
                    {
                        if (Ground.Equals(InFront))
                        {
                            jumpEnabled = false;
                        }
                    }
                }
            }            
        }
        */

        if (Input.GetButtonDown("Jump"))
        {        
            if (characterController.isGrounded)
            {
                jumpEnabled = false;
                isJumping = true;
                doubleJump = true;
                fallSpeed = jumpSpeed;
                StartCoroutine(JumpRoutine());
            }
            else if (doubleJump)
            {
                fallSpeed = jumpSpeed;
                doubleJump = false;
            }    
        }
        moveDirection = new Vector3(currentSpeed, fallSpeed, 0);        
    }

    private IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }   

    private void FixedUpdate()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
