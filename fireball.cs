using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHeight;
    [SerializeField] private GameObject lavaPuddle, location;
    [SerializeField] private Material danger, temp;

    private bool sky = false;
    private GameObject lava; 

    void Start()
    {
        transform.position = location.transform.position;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, speed * 1.75f, 0), ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > maxHeight && !sky)
        {
            sky = true;
            lava = Instantiate(lavaPuddle);
            lava.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -9.2f, 0), transform.rotation);
            lava.GetComponent<Collider>().enabled = false;
            lava.GetComponent<MeshRenderer>().material = danger;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -speed * 0.8f, 0), ForceMode.Impulse);
        }
        else if (transform.position.y < 0)
        {
            sky = false;
        }
    }

    public float getMaxHeight()
    {
        return maxHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Floor") || other.tag.Equals("Lava") && transform.rotation.x == 1 && lava != null) 
        {
            lava.GetComponent<MeshRenderer>().material = temp;
            lava.GetComponent<Collider>().enabled = true;
            Destroy(lava, 8f); 
        }
        else
        {
            Destroy(lava);
        }
        Destroy(gameObject);

    }
}
