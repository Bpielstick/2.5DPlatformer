using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private int livesCount = 3;

    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private GameObject UIManager;
    [SerializeField] private Transform startLocation;
    private UIManager uiManager;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        uiManager = UIManager.GetComponent<UIManager>();
        uiManager.UpdateUI(coinCount, livesCount);
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
        if (characterController.enabled)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }
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

        uiManager.UpdateUI(coinCount, livesCount);
    }

    public void LoseLife ()
    {
        livesCount--;
        uiManager.UpdateUI(coinCount, livesCount);

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        characterController.SimpleMove(new Vector3(0,0,0));
        characterController.enabled = false;   

        yield return new WaitForSeconds(0.5f);
        if (livesCount <= 0)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            transform.position = startLocation.position;
            characterController.enabled = true;

        }
    }

    public void GainLife()
    {
        livesCount++;
        uiManager.UpdateUI(coinCount, livesCount);
    }
}
