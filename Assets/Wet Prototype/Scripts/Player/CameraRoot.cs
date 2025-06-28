using UnityEngine;

public class CameraRoot : MonoBehaviour
{

    [Header("Player Rotation")]
    [SerializeField] float sensitivityY = 1f;
    [SerializeField] float sensitivityX = 1f;

    [Header("Clamp on Y Rotation")]
    [SerializeField] float rotationMinY = -85;
    [SerializeField] float rotationMaxY = 85;

    //Look input variables; 
    float rotationX;
    float rotationY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookControl(Vector2 lookRotationVector)
    {
        //apply sensitivity and store input data
        rotationX += lookRotationVector.x * sensitivityX * Time.deltaTime; //right stick
        rotationY += lookRotationVector.y * sensitivityY * Time.deltaTime; //right stick

        //clamp y rotation
        //rotationY = Mathf.Clamp(rotationY, rotationMinY, rotationMaxY);
        //clamp on z rotation

        //set rotation of player; 
        this.transform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0f);
    }
}
