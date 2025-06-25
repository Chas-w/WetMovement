using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Limb : MonoBehaviour
{
    //EVERY LIMB THAT IS TO BE SEVERED MUST HAVE THIS SCRIPT
    [SerializeField] Limb[] childLimbs;
    [SerializeField] GameObject clone;
    [SerializeField] bool severed;
    GameObject freshlyClonedClone; //this will be a temporary child while the clone behavior is configured

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit()
    {
        if (childLimbs.Length > 0)
        {
            foreach (Limb limb in childLimbs)
            {
                if (limb != null)
                {
                    limb.GetHit(); 
                }
            }
        }

        if (!severed)
        {
            if (clone != null) { Instantiate(clone, transform.position, transform.rotation, transform); } //instantiate a new clone
            transform.localScale = Vector3.zero; //scale down the limb to 0
            severed = true;
        }


       // Destroy(this); //instead of destroying this, we should exit this method and call a method to start growing the limb back
    }
}
