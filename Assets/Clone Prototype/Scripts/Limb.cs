using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Limb : MonoBehaviour
{
    //EVERY LIMB THAT IS TO BE SEVERED MUST HAVE THIS SCRIPT
    [SerializeField] Limb[] childLimbs;
    [SerializeField] GameObject clone;
    [SerializeField] GameObject cloneParent; //this will be the CORE ENEMY object
    [SerializeField] float growSpeed; 

    Vector3 baseScale;
    GameObject freshClone; //this will be a temporary child while the clone behavior is configured
    bool severed; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RegrowThisLimb(severed);
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

        if (!severed && cloneParent.transform.childCount <= 0)
        {
            if (clone != null) { Instantiate(clone, transform.position, transform.rotation, cloneParent.transform); } //instantiate a new clone
            transform.localScale = Vector3.zero; //scale down the limb to 0
            severed = true;
        }

        CloneSetUp();
       // Destroy(this); //instead of destroying this, we should exit this method and call a method to start growing the limb back
    }

    public void RegrowThisLimb(bool isSevered)
    {

        if (isSevered)
        {
            Vector3 currentSize = transform.localScale;


            if (transform.localScale.x < baseScale.x)
            {
                currentSize.x += growSpeed * Time.deltaTime;
                currentSize.y += growSpeed * Time.deltaTime;
                currentSize.z += growSpeed * Time.deltaTime;

                transform.localScale = currentSize;
            }
            if (transform.localScale.x >= baseScale.x)
            {
                transform.localScale = baseScale;
                severed = false; 
            }


        }
    }

    public void CloneSetUp()
    {
 
        if (cloneParent.transform.childCount > 0)
        {
            freshClone = cloneParent.transform.GetChild(0).gameObject;
            CloneGrowth newCloneBehavior = freshClone.GetComponent<CloneGrowth>();

            newCloneBehavior.justCloned = true;
            newCloneBehavior.preservedLimb = gameObject.name;
            Debug.Log(newCloneBehavior.preservedLimb);

            freshClone.transform.parent = null; 
        }
    }
}
