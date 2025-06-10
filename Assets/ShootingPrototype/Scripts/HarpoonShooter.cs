using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using TMPro;

public class HarpoonShooter : MonoBehaviour
{
    [Header("External GameObjects")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject harpoonPrefab;
    [SerializeField] Camera cam;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject inventoryDisplay;

    [Header("Changeable Variables")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] int inventory = 10;
    [SerializeField] float shootForce;

    List<GameObject> harpoons = new List<GameObject>();
    GameObject _selection;
    public ControlsInput getInputs;

    #region Input Data
    private void Awake()
    {
        getInputs = new ControlsInput();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    private void OnEnable()
    {
        getInputs.Enable();
    }
    private void OnDisable()
    {
        getInputs.Disable();
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory > 0 && getInputs.Player.Shoot.WasPressedThisFrame())
        {
            Shoot();
        }
        if (_selection != null && getInputs.Player.Select.WasPressedThisFrame())
        {
            PickUpHarpoon(_selection);
        } 
        
        CheckForHarpoon();
        if(inventoryDisplay != null) inventoryDisplay.GetComponent<TextMeshProUGUI>().text = inventory.ToString();
    }

    void PickUpHarpoon(GameObject currentHarpoon)
    {
        Debug.Log("PickedUp");
        Destroy(currentHarpoon);
        inventory++;
    }

    void CheckForHarpoon()
    {
        if (_selection != null)
        {
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 5);
        if (Physics.Raycast(ray, out hit, 5, layerMask))
        {
            var selection = hit.transform;
            _selection = selection.gameObject;
        }
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 shootDirection = targetPoint - attackPoint.position;
        
        GameObject currentHarpoon = Instantiate(harpoonPrefab, attackPoint.position, Quaternion.identity);
        currentHarpoon.transform.up = shootDirection.normalized;

        currentHarpoon.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * shootForce, ForceMode.Impulse);

        inventory--;
    }
}
