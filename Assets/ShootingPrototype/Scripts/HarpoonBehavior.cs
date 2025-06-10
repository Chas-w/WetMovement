using UnityEngine;

public class HarpoonBehavior : MonoBehaviour
{
    public GameObject player;
    public bool isHeld;

    //Components
    Collider col;
    Rigidbody rb;
    MeshRenderer mr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld)
        {
            this.transform.position = player.transform.position;
        }
    }

    public void SetUpSelf(GameObject player)
    {
        this.player = player;
        isHeld = true;
        Physics.IgnoreCollision(col, player.GetComponent<Collider>(), true);

        if (col.enabled) col.enabled = false;
        if (mr.enabled) mr.enabled = false;
    }

    public void PickUpSelf(GameObject player)
    {
        if (col.enabled) col.enabled = false;
        if (mr.enabled) mr.enabled = false;
    }

    public void Shoot(Vector3 shootDirection, float shootForce)
    {
        rb.AddForce(shootForce * shootDirection * Time.deltaTime, ForceMode.Impulse);
        col.enabled = true;
        mr.enabled = true;
    }

}
