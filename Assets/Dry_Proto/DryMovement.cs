using UnityEngine;

public class DryMovement : MonoBehaviour
{
    [Header("PlayerProperties")]
    [SerializeField] float playerMaxSpeed = 10.0f;
    [SerializeField] float playerAcceleration = 1.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed;


    private CharacterController controller;
    private Vector3 playerVelocity;
    private Transform cameraMainTransform;
    private Rigidbody rigidBody;
    public ControlsInput getInputs;

    Vector3 move;

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
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;

        rigidBody = this.GetComponent<Rigidbody>();
        //rigidBody.maxLinearVelocity = playerMaxSpeed;
    }

    void Update()
    {
        Vector2 movement = getInputs.Player.Move.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y).normalized;

        
        //controller.Move(move * Time.deltaTime * playerMaxSpeed);

        

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        }


    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(move * playerAcceleration, ForceMode.Force);
    }

}
