using UnityEngine;

public class HarpoonBehavior : MonoBehaviour
{
    public GameObject player;
    public bool isHeld;

    //Components
    CapsuleCollider col;
    Rigidbody rb;
    MeshRenderer mr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld)
        {
            this.transform.position = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            rb.isKinematic = true;
            rb.AddForce(rb.GetAccumulatedForce() * -1);
            //Debug.Log("Hit");
        }
    }


}
