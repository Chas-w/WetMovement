using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("PlayerProperties")]
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed; 
 

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Transform cameraMainTransform; 
    public ControlsInput getInputs;

    private void Awake()
    {
        getInputs = new ControlsInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform; 
    }

    void Update()
    {


        Vector2 movement = getInputs.Player.Move.ReadValue<Vector2>();

        // Horizontal input
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        controller.Move(move * Time.deltaTime * playerSpeed);



        /* Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        */

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
    private void OnEnable()
    {
        getInputs.Enable();
    }
    private void OnDisable()
    {
        getInputs.Disable();
    }
}
