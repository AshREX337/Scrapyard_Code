using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform other;
    [SerializeField] private float smoothSpeed = 0.025f;
    private Vector3 offset;


    void Start()
    {
        offset = transform.position;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPos = other.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothPos;
    }
}
