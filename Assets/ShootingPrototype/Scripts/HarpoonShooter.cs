using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class HarpoonShooter : MonoBehaviour
{
    [Header("External GameObjects")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject harpoonPrefab;

    [Header("Changeable Variables")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] int startInventory;
    [SerializeField] float shootForce;

    List<GameObject> harpoons = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        for (int i = 0; i < 10; i++)
        {
            GameObject currentHarpoon = Instantiate(harpoonPrefab, player.transform.position, Quaternion.identity);
            harpoons.Add(currentHarpoon);
            currentHarpoon.GetComponent<HarpoonBehavior>().SetUpSelf(player);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) Shoot();
    }

    void Shoot()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 shootDirection = ray.direction.normalized;

        GameObject currentHarpoon = Instantiate(harpoonPrefab, player.transform.position, Quaternion.identity);
        currentHarpoon.GetComponent<HarpoonBehavior>().SetUpSelf(player);
        currentHarpoon.GetComponent<HarpoonBehavior>().Shoot(transform.forward, shootForce);

        //if(harpoons.Count > 0)
        //{
            //harpoons[0].GetComponent<HarpoonBehavior>().Shoot(transform.forward, shootForce);
            //harpoons[0].GetComponent<HarpoonBehavior>().Shoot(transform.forward, shootForce);
            //harpoons.Remove(harpoons[0]);
        //}


        /*
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20, layerMask))
        {
            var selection = hit.transform;

            Debug.DrawRay(ray.origin, ray.direction, Color.green, 10);

        }
        */
    }
}
