using StarterAssets;
using UnityEngine;
public class MouseLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float minXRotation = -90f;
    [SerializeField] float maxXRotation = 90f;
    [SerializeField] Transform playerMain;
    float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerMain.Rotate(Vector3.up * mouseX);
    }
}
