using StarterAssets;
using UnityEngine;
public class MouseLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float minXRotation = -90f;
    [SerializeField] float maxXRotation = 90f;
    [SerializeField] Transform playerMain;
    float xRotation = 0f;

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
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = getInputs.Player.Look.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = getInputs.Player.Look.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerMain.Rotate(Vector3.up * mouseX);
    }
}
