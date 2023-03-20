using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private float trainSpeed;
    // Update is called once per frame
    void Update()
    {
        var direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position * trainSpeed;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (-transform.forward * direction.normalized.magnitude) * trainSpeed * Time.fixedDeltaTime);
    }
}
