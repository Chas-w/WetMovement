using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneGrowth : MonoBehaviour
{
    public string preservedLimb;
    public bool justCloned; 
    public Enemy enemyManager;

    public Transform[] children; 

    Vector3 baseSize = Vector3.one;
    bool oneVisable; 
    float growSpeed = 1f;

    int finishedGrowingCount = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = GetComponent<Enemy>();
    
    }

    // Update is called once per frame
    void Update()
    {
        MakeOneLimbVisable(justCloned);
        GrowEverythingElse(oneVisable);

        if (justCloned)
        {
            enemyManager.ActivateRagdoll();
            justCloned = false;
        }
    }

    void MakeOneLimbVisable(bool newClone)
    {
        if (newClone && !oneVisable) 
        {
            //Transform[] children = GetComponentsInChildren<Transform>(true).Where(child => child != transform).ToArray();

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].name != preservedLimb)
                {
                    children[i].localScale = Vector3.zero;
                }
            }

            oneVisable = true;
        }

    }

    void GrowEverythingElse(bool readyToGrow)
    {
        if (readyToGrow)
        {
            for (int i = 0; i < children.Length; i++)
            {
                Vector3 currentSize = children[i].localScale;

                if (children[i].localScale.x <= baseSize.x)
                {
                    currentSize.x += growSpeed * Time.deltaTime;
                    currentSize.y += growSpeed * Time.deltaTime;
                    currentSize.z += growSpeed * Time.deltaTime;

                    children[i].localScale = currentSize;
                }
                else
                {
                    children[i].localScale = baseSize;
                    Debug.Log(finishedGrowingCount);
                }
            }

        }


    }
}
