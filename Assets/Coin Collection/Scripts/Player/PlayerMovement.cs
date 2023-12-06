using StarterAssets;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{

    //public float speed = 5f;
    public AudioSource pickupSound;
    public AudioSource jumpSound;
    public TextMeshProUGUI coinText; // Reference to the Text UI element
    public int totalCoins = 0;
    public GameObject gameoverpanel;
    //public Rigidbody rb;

    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        totalCoins = 0;
    }
    public void ResetCoin()
    {
        totalCoins = 0;
        UpdateCoinText();
    }

    public void CoinCollect()
    {
        totalCoins++;
        GameManager.Instance.CollectCoin();
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text =totalCoins + " / " + GameManager.Instance.coin.Count;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //// Player movement using arrow keys
    //    //float horizontalInput = -Input.GetAxis("Horizontal");
    //    //float verticalInput = -Input.GetAxis("Vertical");
    //    //Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
    //    //transform.Translate(movement);


    //    //// Keep player within boundaries
    //    //float xBound = Mathf.Clamp(transform.position.x, -dist, dist);
    //    //float zBound = Mathf.Clamp(transform.position.z, -dist, dist);
    //    //transform.position = new Vector3(xBound, transform.position.y, zBound);


    //}


    public CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float playerSprintSpeed = 6;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private float _rotationVelocity;
    private float _verticalVelocity;

    void Update()
    {
        GroundedCheck();
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        //Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //Vector3 rotation = new Vector3(0,Input.GetAxis("Horizontal"), 0);
        //controller.Move(move * Time.deltaTime * playerSpeed);

        //transform.eulerAngles += rotation;
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        //// Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //    jumpSound.Play();
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);

        Move();
        JumpAndGravity();   
    }
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        groundedPlayer = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

        
    }
    private float _speed;
   
    private float _targetRotation = 0.0f;
  
    private float _terminalVelocity = 53.0f;
    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = playerSpeed;
        if(Input.GetButton(KeyCode.LeftShift.ToString()))
        {
            targetSpeed = playerSprintSpeed;
        }
        else
        {
            targetSpeed = playerSpeed;
        }
        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0

        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveDirection == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f,controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude =  1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * 10);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
               
        
       

        // normalise input direction
        Vector3 inputDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (moveDirection != Vector2.zero)
        {
           
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              Camera.main.transform.eulerAngles.y;
           
                
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                0.2f);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
       controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

       
    }

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    public float FallTimeout = 0.15f;
    public float JumpHeight =2f;
    public float JumpTimeout = 0.50f;
    public float Gravity = -15.0f;
    private void JumpAndGravity()
    {
        if (groundedPlayer)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // update animator if using character
          

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (Input.GetButtonDown("Jump") && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * -15);
                //SoundManager.Instance.jumpSound.Play();
                // update animator if using character
                jumpSound.Play();
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick Up"))
        {
            pickupSound.Play();
            Debug.Log("Item collected!");
            CoinCollect();
            other.gameObject.SetActive(false); // Destroy the pickup when collected by the player.            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
