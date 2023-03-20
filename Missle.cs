using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 distance;
    [SerializeField] private GameObject Explode;
    [SerializeField] private float droneSpeed;

    // Update is called once per frame
    void Update()
    {
        var direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position * droneSpeed;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (transform.forward * direction.normalized.magnitude) * droneSpeed * Time.fixedDeltaTime);
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != ("Enemy"))
        {
            var ex = Instantiate(Explode);
            ex.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Destroy(ex, 0.5f);
        }
    }
}
