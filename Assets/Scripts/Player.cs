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
    [SerializeField] private int coinCount = 0;

    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private GameObject UIManager;
    private UIManager uiManager;
    private CharacterController characterController;
    private LayerMask layerMask = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        uiManager = UIManager.GetComponent<UIManager>();
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
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }   

    public void Pickup (string PickupType)
    {
        switch (PickupType)
        {
            case "Coin":
                coinCount++;
                break;
            default:
                break;           
        }

        uiManager.UpdateUI(coinCount);
    }
}
