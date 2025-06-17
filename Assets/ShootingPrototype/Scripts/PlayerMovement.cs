using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    [SerializeField] float speed = 12f;

    public ControlsInput getInputs;

    #region Input Data
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
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = getInputs.Player.Move.ReadValue<Vector2>().x;
        float z = getInputs.Player.Move.ReadValue<Vector2>().y;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move *  speed * Time.deltaTime);
    }
}
