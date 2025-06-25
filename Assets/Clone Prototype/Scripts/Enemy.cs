using NUnit.Framework;
using System.Collections; 
using UnityEngine;
using System.Collections.Generic; 

public class Enemy : MonoBehaviour
{
    //THIS SCRIPT SHOULD BE ATTATCHED TO THE CORE ENEMY OBJECT
    //tutorial referenced: https://www.youtube.com/watch?v=oqcZXTBhVJY
    Animator animator; 

    List<Rigidbody> ragdollRigids;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        ragdollRigids = new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>());  
        ragdollRigids.Remove(GetComponent<Rigidbody>());

      DeActivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateRagdoll()
    {
        animator.enabled = false;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = true; 
            ragdollRigids [i].isKinematic = false;
        }
    }

    public void DeActivateRagdoll()
    {
        animator.enabled = true;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = false;
            ragdollRigids[i].isKinematic = true;
        }
    }
}
