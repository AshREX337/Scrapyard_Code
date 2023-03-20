using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject dropController;
    [SerializeField] private GameObject heldController;
    private int check = 1;

    void Start()
    {
        dropController.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(check == 1)
            {
                dropController.SetActive(true); 
            }
            else if (check == -1)
            {
                dropController.GetComponent<PlaceThing>().destroy();
                dropController.GetComponent<PlaceThing>().switchPlacement();
                dropController.SetActive(false);
            }   
            check *= -1;
        }
    }
}
