using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class LimbTracker : MonoBehaviour
{
    [Header("List of Limbs")]
    public GameObject[] limbs;
    public bool[] limbsVisible;

    [Header("Limb Size Data")]
    [SerializeField] float limbSizeMin;
    [SerializeField] float limbSizeMax;
    [SerializeField] float limbGrowthSpeed;

    [SerializeField] bool[] shrink;
    Vector3 limbShrinker;

    [Header("Clone Data")]
    public GameObject cloneHolder;
    public GameObject cloneSource; 
    public Camera cam;
    public bool triggerClone;

    [SerializeField] float tempPush = 20f;

    int limbLost;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindAnyObjectByType<Camera>();
        //limbLost = -1;
    }

    // Update is called once per frame
    void Update()
    {
        LimbVisability();
        CloneBehavior(limbLost); //if(limbLost != -1)  -> find when to revert it back to -1, should be after the clone is already done.

    }

    private void LimbVisability()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            if (limbs[i] != null)
            {
                #region temporary mouse cast
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo) && Input.GetMouseButtonDown(0))
                {
                    if (hitInfo.transform.position == limbs[i].transform.position)
                    {
                        limbsVisible[i] = false;
                        limbLost = i; //add a force to the rigidbody, should be a vector from original body to limb, shoots off and lands on the ground
                        triggerClone = true;
                        Instantiate(cloneSource, cloneHolder.transform, false);
                    }
                }
                #endregion
                LimbGrowth(i);


            }
        }
    }

    private void CloneBehavior(int limb)
    {
        int growthNumber = 0; 
            for (int i = 0; i < limbs.Length; i++)
            { 
                if (triggerClone)
                {
                    if (i == limb)
                    {
                        cloneHolder.GetComponentInChildren<LimbTracker>().limbsVisible[i] = true; 
                    } 
                    else
                    {
                        cloneHolder.GetComponentInChildren<LimbTracker>().limbsVisible[i] = false;
                    }
                }
                if (!cloneHolder.GetComponentInChildren<LimbTracker>().shrink[i])
                {
                    growthNumber++;
                }
            
            }

            if (growthNumber >= 6)
            {
                triggerClone = false;
                cloneHolder.transform.DetachChildren();
            }

    }
    private void LimbGrowth(int limb)
    {

        if (limbsVisible[limb] == false)
        {

            if (!shrink[limb])
            {
                limbShrinker = new Vector3(limbSizeMin, limbSizeMin, limbSizeMin);
                shrink[limb] = true;
            }

            if (limbShrinker.x <= limbSizeMax)
            {
                limbShrinker.x += Time.deltaTime * limbGrowthSpeed;
                limbShrinker.y += Time.deltaTime * limbGrowthSpeed;
                limbShrinker.z += Time.deltaTime * limbGrowthSpeed;
                limbs[limb].transform.localScale = limbShrinker;

            }
            else
            {
                limbsVisible[limb] = true;
                shrink[limb] = false;
            }
        }
        if (limbsVisible[limb])
        {
            limbs[limb].transform.localScale = new Vector3(limbSizeMax, limbSizeMax, limbSizeMax);
        }
    }
}
