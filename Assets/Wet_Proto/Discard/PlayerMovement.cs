using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Game testing")]
    public InputActionReference test;

    [Header("Player Properties")]
    public Rigidbody rigidBody;
    public float moveSpeedMax;
    public ControlsInput GetInputs; 
    public InputActionReference swim; //access the swim movement action mapping


    [Header("Animation Properties")]
    public Animator animator;

    Vector3 moveDirection;
    bool vertToggled;
    float xAcceleration = 0; 
    float yAcceleration = 0;


    private void Awake()
    {
        GetInputs = new ControlsInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = GetInputs.Player.Move.ReadValue<Vector2>(); //assign the values of the swim action mapping to a vector 
    }

    private void FixedUpdate()
    {

        DirectionalSpeed();
        vertToggled = GetInputs.Player.VertToggle.IsPressed();
        animator.SetFloat("Speed", yAcceleration / 100); //why isn't this working
    }

    private void OnEnable()
    {
       GetInputs.Enable();
    }
    private void OnDisable()
    {
        GetInputs.Disable(); 
    }

    void DirectionalSpeed() //need help with this section
    {
        if (vertToggled)
        {
            rigidBody.linearVelocity = new Vector3(moveDirection.x * xAcceleration, moveDirection.y * yAcceleration, 0); //apply the swim vector to the rigid body (vertical)
        }
        else
        {
            rigidBody.linearVelocity = new Vector3(moveDirection.x * xAcceleration, 0, moveDirection.y * yAcceleration); //apply the swim vector to the rigid body (horizontal)
        }

        if (GetInputs.Player.Move.IsPressed())
        {
            if (moveDirection.x != 0)
            {
                if (xAcceleration < moveSpeedMax)
                {
                    xAcceleration += 2*Time.deltaTime;
                }
            }

            if (moveDirection.y != 0)
            {
                if (yAcceleration < moveSpeedMax)
                {
                    yAcceleration += 2*Time.deltaTime;
                }
            }
        }
        else
        {
            if (xAcceleration > 0)
            {
                xAcceleration -= Time.deltaTime;
            }
            else
            {
                xAcceleration += Time.deltaTime; 
            }

            if (yAcceleration > 0)
            {
                yAcceleration -= Time.deltaTime;
            }
            else
            {
                yAcceleration += Time.deltaTime;
            }  
        }
  
        

    }
}
