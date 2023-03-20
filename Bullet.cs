using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform parent;

    private void Start()
    {
        transform.position = parent.position;
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void SetPos(Transform pos)
    {
        parent = pos;
    }
}
