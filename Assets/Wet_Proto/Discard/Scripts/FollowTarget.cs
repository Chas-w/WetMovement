using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationalSpeed = 10f;
    [SerializeField] float bottomClamp = -40f;
    [SerializeField] float topClamp = 70f;

    float cinemachineTargetPitch;
    float cinemachinetargetYaw;
    public ControlsInput getInputs;


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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        CameraLogic();
    }

    void CameraLogic()
    {
        float mouseX = getInputs.Player.Look.ReadValue<Vector2>().x;
        float mouseY = getInputs.Player.Look.ReadValue<Vector2>().y;

        cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        cinemachinetargetYaw = UpdateRotation(cinemachinetargetYaw, mouseX, float.MinValue, float.MaxValue, false);
    }

    void ApplyRotations(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }


    float GetLookInput(float lookValue)
    {
        return lookValue * rotationalSpeed * Time.deltaTime;
    }
}
