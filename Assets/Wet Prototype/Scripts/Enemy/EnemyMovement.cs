using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Parts")]
    [SerializeField] Transform enemyGraphics;

    Transform playerTransform; //it will cache faster if we reference the transform

    [Header("Player Rotation")]
    float sensitivityY = 1f;
    float sensitivityX = 1f;
    float turnTime = 5; //how long it takes for the player to snap to the direction

    [Header("Clamp on Y Rotation")]
    float rotationMinY = -85;
    float rotationMaxY = 85;

    //Look input variables; 
    float rotationX;
    float rotationY;

    [Header("Enemy Movement")]
    [SerializeField] float maxSpeed = 350;
    [SerializeField] float acceleration = 75; //how much the speed increases by
    [SerializeField] float deceleration = 100; //how much the speed decreases by when given player input 
    [SerializeField] float drag = 80; //how much the speed decreases when given no input

    //move variables
    Rigidbody rigidBody;
    Vector3 playerDistance;
    Vector3 moveDirection;
    Vector2 moveVector;
    float moveY;
    float speed;
    bool playerInRange;

    //gameobjects
    GameObject player;

    //AnimationVariables
    [Header("Animation Data")]
    [SerializeField] Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = this.transform;
        rigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerInRange) {

            playerDistance = player.transform.forward - this.transform.position;
            moveDirection = playerDistance.normalized;

            if (enemyGraphics.transform.forward != moveDirection)
            {
                //enemyGraphics.transform.forward = Vector3.MoveTowards(enemyGraphics.transform.forward, moveDirection, Time.deltaTime);
                //transform.forward = moveDirection;
            }
        }

    }

    private void FixedUpdate()
    {
        MoveControl(moveVector);
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

        rigidBody.AddForce(moveDirection * speed);
        //rigidBody.AddForce(playerTransform.right * strafeSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInRange = true;
            Debug.Log("seenPlayer");
            moveVector = new Vector2(0, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInRange = false;
            moveVector = new Vector2(0, 0);
        }
    }
}
