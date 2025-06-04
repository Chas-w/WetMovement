using UnityEngine;

public class WetController : MonoBehaviour
{
    public ControlsInput getInputs;

    [Header("Properties")]
    [Tooltip("How much the player ramps up or down")]
    public float strokeIncrement = 1f;
    [Tooltip("Max speed at 100%")]
    public float maxThrust = 200f;
    [Tooltip("How responsive the plalyer is when rolling, pitching, and yawing")]
    public float responsiveness = 10f;

    float stroke;
    float roll;
    float pitch;
    float yaw; 

    Rigidbody rigidBody;

    private void Awake()
    {
        getInputs = new ControlsInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rigidBody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        getInputs.Enable();
    }
    private void OnDisable()
    {
        getInputs.Disable();
    }
    float responseModifier
    {
        get
        {
           return (rigidBody.mass / 10f) * responsiveness; //takes mass into account when configuring movement; 
        }
    }
    private void HandleInputs()
    {
        Vector2 moveVector = getInputs.Player.Move.ReadValue<Vector2>();
        Vector2 lookVector = getInputs.Player.Look.ReadValue<Vector2>();

        if (lookVector.x > 0) Debug.Log("hi");

        roll = 0;
        pitch = lookVector.y;
        yaw = lookVector.x;

        

        if (moveVector.x > 0)
        {
            stroke += strokeIncrement;
            //Debug.Log("x");
        } 
        else if (moveVector.x < 0)
        {
            stroke -= strokeIncrement;
            //Debug.Log("o");
        }

        stroke = Mathf.Clamp(stroke, -100f, 100f);
        Debug.Log(roll);
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(transform.forward * maxThrust * stroke);



        rigidBody.AddTorque(transform.up * yaw * responseModifier);
        rigidBody.AddTorque(transform.right * pitch * responseModifier);
        rigidBody.AddTorque(transform.forward * roll * responseModifier);

    }

}
