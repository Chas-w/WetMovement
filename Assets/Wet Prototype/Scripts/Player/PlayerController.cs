using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ControlsInput getInputs;

    [Header("Player Movement")]
    public SwimMovement swimMovement;
    public CameraRoot cameraRoot;
    Vector2 lookRotation;
    Vector2 moveAxis;

    [Header("Breath Control")]
    public SwimOxygen oxygenControl;
    bool takeBreath; 


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
    void InputAssignment()
    {
        moveAxis = getInputs.Player.Move.ReadValue<Vector2>(); //left stick
        lookRotation = getInputs.Player.Look.ReadValue<Vector2>(); //right stick
        takeBreath = getInputs.Player.Breathing.WasPressedThisFrame(); 
    }
    #endregion

    void Update()
    {
        InputAssignment(); 
        //swimMovement.LookControl(lookRotation);
        cameraRoot.LookControl(lookRotation);
        oxygenControl.PlayerBreathe(takeBreath);
        
        cameraRoot.gameObject.transform.position = swimMovement.gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        swimMovement.MoveControl(moveAxis);
    }


}
