
using System;
using System.Diagnostics;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlaceThing : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    private GameObject currentPlaceableObject, arrow;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Material placed;
    [SerializeField] private Camera cam;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private Boolean placing = false;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPlaceableObject);
                    Destroy(arrow);
                    currentPrefabIndex = -1;
                }
                else
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                        Destroy(arrow); 
                    }

                    placing = true;
                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                    currentPlaceableObject.GetComponent<Collider>().enabled = false;
                        arrow = Instantiate(arrowPrefab);
                    currentPrefabIndex = i;
                }

                break;
            }
        }
    }

    public Boolean isPlacing()
    {
        return placing;
    }

    public void switchPlacement()
    {
        if (placing)
        {
            placing = false;
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 v = new Vector3(hitInfo.point.x, 10, hitInfo.point.z);
            currentPlaceableObject.transform.SetPositionAndRotation(new Vector3(hitInfo.point.x, 25, hitInfo.point.z), Quaternion.identity);
            currentPlaceableObject.GetComponent<placementCollision>().setPlaced(placed);
            arrow.transform.SetPositionAndRotation(v, Quaternion.identity);

        }
    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        arrow.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(arrow);
            currentPlaceableObject.GetComponent<Collider>().enabled = true ;
            currentPlaceableObject.GetComponent<placementCollision>().resetPlaced();
            Vector3 v = new Vector3(currentPlaceableObject.transform.position.x, currentPlaceableObject.transform.position.y + 75, currentPlaceableObject.transform.position.z);
            currentPlaceableObject.transform.SetPositionAndRotation(v, currentPlaceableObject.transform.rotation);

            currentPlaceableObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,-75,0), ForceMode.Impulse);
            currentPlaceableObject.GetComponent<placementCollision>().enabled = false;
            currentPlaceableObject = null;
            mouseWheelRotation = 0;
            placing = false;

        }

    }

    public void destroy()
    {
        Destroy(currentPlaceableObject);
        Destroy(arrow);
    }


}