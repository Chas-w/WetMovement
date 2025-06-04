using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public ControlsInput getInputs;

    [Header("Player Properties")]
    [SerializeField] Transform cam;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float moveForce;
    [SerializeField] float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    
    Rigidbody rigidBody;
    Vector3 direction;
    Vector3 moveDir;

    bool canMove = false;

    private void Awake()
    {
        getInputs = new ControlsInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void OnEnable()
    {
        getInputs.Enable();
    }
    private void OnDisable()
    {
        getInputs.Disable();
    }


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.maxLinearVelocity = maxSpeed;
    }

    private void Update()
    {
        Vector2 movement = getInputs.Player.Move.ReadValue<Vector2>();
        direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y ; 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;
            canMove = true;
        } else
        {
            canMove = false;
        }

        //if (rigidBody.linearVelocity.magnitude > maxSpeed) canMove = false;

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigidBody.AddForce(moveDir.normalized * moveForce);
        } 
    }
}
