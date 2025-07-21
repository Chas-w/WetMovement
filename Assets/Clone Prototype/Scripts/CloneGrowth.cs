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
    float ragdollTimer = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {

    }
    void Start()
    {
        enemyManager = GetComponent<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {
        if (justCloned)
        {
            //enemyManager.ActivateRagdoll(); //turn on ragdoll so that the limb will fall
            MakeOneLimbVisable(); //only enlarge the limb that was severed to make it look like 1 limb fell off
            Debug.Log("justcloned");

            justCloned = false; //exit this loop
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
        if (oneVisable)
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
            }
        }


    }
}
