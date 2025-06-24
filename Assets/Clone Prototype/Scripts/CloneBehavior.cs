using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
public class CloneBehavior : MonoBehaviour
{
    [Header("List of Limbs")]
    public GameObject[] limbs;
    public bool[] limbsVisible;


    [Header("Limb Size Data")]
    [SerializeField] float limbSizeMin;
    [SerializeField] float limbSizeMax;
    [SerializeField] float limbGrowthSpeed;

    [SerializeField] bool[] shrinking;
    Vector3 limbShrinker;

    [Header("Clone Data")]
    public GameObject cloneHolder;
    public GameObject cloneSource;
    public Camera cam;
    public bool triggerClone;

    [SerializeField] float tempPush = 20f;

    int limbLost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = FindAnyObjectByType<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckLimbsHit();
        Clone(limbLost); 
    }

    void CheckLimbsHit()
    {
        for (int i = 0; i < limbs.Length; i++) //this section checks if a limb has been shot and cycles through the limbs
        {
            if (limbs[i] != null)
            {
                #region temporary mouse cast 
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo) && Input.GetMouseButtonDown(0))
                {
                    if (hitInfo.transform.position == limbs[i].transform.position) //if the limb has been shot
                    {
                        limbsVisible[i] = false; //set the limb to invisible briefly
                        limbLost = i; //mark this limb as lost
                        triggerClone = true; //start the clone process
                        Instantiate(cloneSource, cloneHolder.transform, false); //instantiate the new limb
                    }
                    Debug.Log(hitInfo.transform.name);
                }
                #endregion
                LimbGrowth(i);
            }
        }
    }

    private void Clone(int limb)
    {
        int growthNumber = 0;
        for (int i = 0; i < limbs.Length; i++)
        {
            if (triggerClone) //if trigger clone was set to true by the check limbs hit function
            {
                if (i == limb) //and if i is == to the limb lost variable set in the check limbs hit function
                {
                    cloneHolder.GetComponentInChildren<CloneBehavior>().limbsVisible[i] = true; //set this limb to visable and every other limb to invisible (this is the limb that is detatched)
                }
                else
                {
                    cloneHolder.GetComponentInChildren<CloneBehavior>().limbsVisible[i] = false;
                }
            }
            if (!cloneHolder.GetComponentInChildren<CloneBehavior>().shrinking[i])
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

    void LimbGrowth(int limb)
    {

        if (limbsVisible[limb] == false) //if the limb was marked to be turned off
        {

            if (!shrinking[limb]) //if not already shrinking the limb
            {
                limbShrinker = new Vector3(limbSizeMin, limbSizeMin, limbSizeMin); //set the new size for the limb to shrink to 
                shrinking[limb] = true; //set the limb as shrinking
            }

            if (limbShrinker.x <= limbSizeMax) //shrink the limb until it is the set shrunken size
            {
                limbShrinker.x += Time.deltaTime * limbGrowthSpeed;
                limbShrinker.y += Time.deltaTime * limbGrowthSpeed;
                limbShrinker.z += Time.deltaTime * limbGrowthSpeed;
                limbs[limb].transform.localScale = limbShrinker;

            }
            else
            {
                limbsVisible[limb] = true; //once it is the size set the limb back to visible
                shrinking[limb] = false; //mark that it isn't shrinking
            }
        }
        if (limbsVisible[limb])
        {
            limbs[limb].transform.localScale = new Vector3(limbSizeMax, limbSizeMax, limbSizeMax); //set the limb to be exactly the maz size
        }
    }
}
