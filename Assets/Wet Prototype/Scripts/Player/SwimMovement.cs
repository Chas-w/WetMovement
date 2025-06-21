using UnityEngine;

public class SwimMovement : MonoBehaviour
{

    [SerializeField] Transform cameraRoot;
    [SerializeField] Transform playerGraphics;

    Transform playerTransform; //it will cache faster if we reference the transform
    Transform directionTransform;

    [Header("Player Rotation")]
    [SerializeField] float sensitivityY = 1f;
    [SerializeField] float sensitivityX = 1f;

    [Header("Clamp on Y Rotation")]
    [SerializeField] float rotationMinY = -85;
    [SerializeField] float rotationMaxY = 85;

    //Look input variables; 
    float rotationX;
    float rotationY;

    [Header("Player Movement")]
    [SerializeField] float maxSpeed = 350;
    [SerializeField] float acceleration = 75; //how much the speed increases by
    [SerializeField] float deceleration = 100; //how much the speed decreases by when given player input 
    [SerializeField] float drag = 80; //how much the speed decreases when given no input

    //move variables
    Rigidbody rigidBody;
    float moveY;
    float speed;

    //AnimationVariables
    [Header("Animation Data")]
    [SerializeField] Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = this.transform;
        rigidBody = this.GetComponent<Rigidbody>();
    }

    public void MoveControl(Vector2 moveAxisVector)
    {
        anim.SetFloat("Speed", speed);

        //store input data
        moveY = moveAxisVector.y;

        //movement on the Y
        if (moveY > 0)
        {
            speed += acceleration * Time.deltaTime;

            if (playerGraphics.transform.rotation != cameraRoot.transform.rotation)
            {
                playerGraphics.transform.rotation = Quaternion.Lerp(playerGraphics.transform.rotation, cameraRoot.transform.rotation, Time.deltaTime * 5);
            }
            
        }
        else if (moveY < 0)
        {
            speed -= deceleration * Time.deltaTime;
        }
        else if (moveY == 0)
        {
            speed -= drag * Time.deltaTime;
        }


        speed = Mathf.Clamp(speed, 0, maxSpeed);
        //strafeSpeed = Mathf.Clamp(strafeSpeed, -maxStrafeSpeed, maxStrafeSpeed);

        rigidBody.AddForce(cameraRoot.transform.forward * speed);
        //rigidBody.AddForce(playerTransform.right * strafeSpeed);
    }

    public void LookControl(Vector2 lookRotationVector)
    {
        //apply sensitivity and store input data
        rotationX += lookRotationVector.x * sensitivityX; //right stick
        rotationY += lookRotationVector.y * sensitivityY; //right stick

        //clamp y rotation
        rotationY = Mathf.Clamp(rotationY, rotationMinY, rotationMaxY);
        //clamp on z rotation

        //set rotation of player; 
        //playerTransform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0f);
    }
}
