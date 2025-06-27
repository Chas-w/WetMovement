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
    [SerializeField] GameObject hips;
    Transform preservedLimbTransform; 


    Vector3 baseSize = Vector3.one;
    Vector3 preservedSize = new Vector3(100,100, 100);
    bool oneVisable; 
    float growSpeed = .5f;
    float fixScale = 1; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        baseSize = hips.transform.localScale;

    }
    void Start()
    {
        enemyManager = GetComponent<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {

        MakeOneLimbVisable();

        if (justCloned)
        {
            hips.transform.localScale = new Vector3(hips.transform.localScale.x * .01f, hips.transform.localScale.x * .01f, hips.transform.localScale.x * .01f);
            enemyManager.ActivateRagdoll();
            justCloned = false;
        }
    }
    private void FixedUpdate()
    {
        GrowEverythingElse();

    }

    void MakeOneLimbVisable()
    {
        if (justCloned && !oneVisable) 
        {

            Debug.Log(limbs.Length);
            for (int i = 0; i < limbs.Length; i++)
            {
                if (limbs[i].name == preservedLimbName)
                {
                    limbs[i].localScale = preservedSize;
                    preservedLimbTransform = limbs[i];
                    Debug.Log(preservedLimbTransform.name);
                    oneVisable = true;

                }

            }

            

        }

    }

    void GrowEverythingElse()
    {
        if (oneVisable)
        {

            Vector3 currentSize = hips.transform.localScale;
            Vector3 preservedLimbSize = preservedLimbTransform.localScale;

            if (hips.transform.localScale.x <= baseSize.x)
            {
                currentSize.x += growSpeed * Time.deltaTime;
                currentSize.y += growSpeed * Time.deltaTime;
                currentSize.z += growSpeed * Time.deltaTime;

                
                hips.transform.localScale = currentSize;
            } else { hips.transform.localScale = baseSize; }

            preservedLimbTransform.localScale = new Vector3(fixScale / hips.transform.localScale.x, fixScale / hips.transform.localScale.y, fixScale / hips.transform.localScale.z); ;

            if (hips.transform.localScale == baseSize)
            {
                preservedLimbTransform.localScale = baseSize; 
                oneVisable = false; 
            }
        }


    }
}
