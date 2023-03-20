using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldThing : MonoBehaviour
{
    [SerializeField] private GameObject[] hotbar;
    [SerializeField] private GameObject select;
    [SerializeField] private Vector3[] positions;
    private GameObject currentWeapon;
    private int check = 1;


    // Update is called once per frame
    void Start()
    {
        Instantiate(hotbar[0], transform.position, transform.rotation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            check *= -1;
        }
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i) && check != -1)
            {
                Destroy(currentWeapon);
                currentWeapon = Instantiate(hotbar[i], transform.position, transform.rotation);
                select.GetComponent<Rigidbody>().MovePosition(positions[i]);
            }
        }

        if(currentWeapon != null)
        {
            currentWeapon.transform.parent = this.transform;
        }
    }
}
