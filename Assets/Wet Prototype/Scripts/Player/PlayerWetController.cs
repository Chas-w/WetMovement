using Unity.VisualScripting;
using UnityEngine;

public class PlayerWetController : MonoBehaviour
{
    public ControlsInput getInputs;

    Transform playerTransform; //it will cache faster if we reference the transform

    [Header("Player Rotation")]
    [SerializeField] float sensitivityY = 1f;
    [SerializeField] float sensitivityX = 1f;
    [SerializeField] float sensitivityZ = 1f; 

    [Header("Clamp on Y Rotation")]
    [SerializeField] float rotationMinY;
    [SerializeField] float rotationMaxY;

    [Header("Clamp on Z Rotation")]
    [SerializeField] float rotationMinZ;
    [SerializeField] float rotationMaxZ;

    //Look input variables; 
    Vector2 lookRotation;
    float rotationX;
    float rotationY;
    float rotationZ;

    [Header("Player Movement")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration = 10f; //how much the speed increases by
    [SerializeField] float deceleration = 20f; //how much the speed decreases by when given player input 
    [SerializeField] float drag = 15f; //how much the speed decreases when given no input


    //move variables
    Rigidbody rigidBody;
    Vector2 moveAxis;
    float moveY;
    float speed;

    //AnimationVariables
    [SerializeField] Animator anim;

    private void Awake()
    {
        getInputs = new ControlsInput();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    private void OnEnable()
    {
        getInputs.Enable();
    }
    private void OnDisable()
    {
        getInputs.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = this.transform;
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputAssignment(); 
        LookControl();

        anim.SetFloat("Speed", speed);
    }
    private void FixedUpdate()
    {
        MoveControl();
    }

    void InputAssignment()
    {
        moveAxis = getInputs.Player.Move.ReadValue<Vector2>(); //left stick
        lookRotation = getInputs.Player.Look.ReadValue<Vector2>(); //right stick
    }

    void MoveControl()
    {
        //store input data
        moveY = moveAxis.y;

        if (moveY > 0)
        {
            speed += acceleration * Time.deltaTime; 
        } else if (moveY < 0)
        {
            speed -= deceleration * Time.deltaTime;
        } else if (moveY == 0)
        {
            speed -= drag * Time.deltaTime;
        }

        Debug.Log(speed);

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        rigidBody.AddForce(playerTransform.forward * speed);
    }

    void LookControl()
    {
        //apply sensitivity and store input data
        rotationZ += moveAxis.x * sensitivityZ; // left stick
        rotationX += lookRotation.x * sensitivityX; //right stick
        rotationY += lookRotation.y * sensitivityY; //right stick

        //clamp y rotation
        rotationY = Mathf.Clamp(rotationY, rotationMinY, rotationMaxY);
        //clamp on z rotation
        rotationZ = Mathf.Clamp(rotationZ, rotationMinZ, rotationMaxZ);

        //set rotation of player; 
        playerTransform.localRotation = Quaternion.Euler(-rotationY, rotationX, -rotationZ /*THIS SHOULD BE BASED ON MOVE CONTROL'S MOVE X*/);
    }
}
