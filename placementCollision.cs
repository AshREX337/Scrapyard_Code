using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placementCollision : MonoBehaviour {

    [SerializeField] private Material placed;
    [SerializeField] private MeshRenderer mesh;
    private Material temp;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Awake()
    {
        temp = placed;
        Debug.Log(temp.ToString());
    }
    private void Update()
    {
        mesh.material = placed;
    }

    public void setPlaced(Material mat)
    {
        placed = mat;
    }

    public void resetPlaced()
    {
        placed = temp;
        mesh.material = placed;
    }
}
