using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CloneGrowth : MonoBehaviour
{
    public string preservedLimbName;
    public bool justCloned;
    public Enemy enemyManager;

    [SerializeField] Transform[] limbs;
    [SerializeField] GameObject baseBody;
    Transform preservedLimbTransform; 

    Vector3 baseSize = Vector3.one;
    Vector3 preservedSize = new Vector3(100,100, 100);
    bool oneVisable; 
    float growSpeed = .5f;
    float fixScale = 1;

    Rigidbody parentRigidbody;

    [Header("Attributes")]
    [SerializeField] float ragdollTimer = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {

    }
    void Start()
    {
        enemyManager = GetComponent<Enemy>();
        parentRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (justCloned)
        {
            enemyManager.ActivateRagdoll(); //turn on ragdoll so that the limb will fall

            //ENABLE PARENT RIGIDBODY
            parentRigidbody.isKinematic = false;
            parentRigidbody.useGravity = true;

            //New Vector needs to be replaced with the direction opposing that which the player shot, a force needs to be applied to mimic
            parentRigidbody.AddForce(new Vector3(0, 0, 1) * 5, ForceMode.Impulse);

            MakeOneLimbVisable(); //only enlarge the limb that was severed to make it look like 1 limb fell off
            justCloned = false; //exit this loop
        }

        if(oneVisable) //Time spent being JUST a limb
        {
            ragdollTimer -= Time.deltaTime;
        }


    }
    private void FixedUpdate()
    {
       GrowEverythingElse();

    }

    void MakeOneLimbVisable()
    {
        if (!oneVisable) 
        {

            Debug.Log(limbs.Length);
            for (int i = 0; i < limbs.Length; i++) //cycle through limbs
            {
                if (limbs[i].name == preservedLimbName) //with the matching limb
                {
                    limbs[i].localScale = preservedSize; //enlargen it
                    preservedLimbTransform = limbs[i];
                    oneVisable = true;
                }
            }
        }

    }

    void GrowEverythingElse()
    {
        if (oneVisable && ragdollTimer < 0)
        {
     
            Vector3 currentSize = baseBody.transform.localScale; //set sizes
            Vector3 preservedLimbSize = preservedLimbTransform.localScale;

            if (baseBody.transform.localScale.x <= baseSize.x) //if the rest of the body is still shrunk grow it
            {
                currentSize.x += growSpeed * Time.deltaTime;
                currentSize.y += growSpeed * Time.deltaTime;
                currentSize.z += growSpeed * Time.deltaTime;

                
                baseBody.transform.localScale = currentSize;
            } else { baseBody.transform.localScale = baseSize; }

            preservedLimbTransform.localScale = new Vector3(fixScale / baseBody.transform.localScale.x, fixScale / baseBody.transform.localScale.y, fixScale / baseBody.transform.localScale.z); //keep the scale of the preserved limb fixed

            if (baseBody.transform.localScale == baseSize) //exit the loop
            {
                preservedLimbTransform.localScale = baseSize; 
                oneVisable = false;

                //RESET PARENT TO BE UPRIGHT AND NORMAL
                this.GetComponent<CapsuleCollider>().enabled = false;
                parentRigidbody.useGravity = false;
                parentRigidbody.isKinematic = true;
                enemyManager.DeActivateRagdoll();
                
                this.transform.up = Vector3.up;

                //enemyManager.DeActivateRagdoll();
            }
        }


    }
}
