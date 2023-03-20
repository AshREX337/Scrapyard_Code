using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform hand;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = hand.position + new Vector3(0, -0.5f, 0);
        gameObject.transform.rotation = hand.rotation;
        gameObject.transform.Rotate(new Vector3(60, 0 , 0));
    }
}
