using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deus : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab, trainPrefab, hitIndicator;
    private GameObject drone, train, hit;
    private int trainCount, droneCount;
    private bool trainSpawn, droneSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            trainSpawn = true;
        }

        if (trainSpawn && Time.frameCount % 100 == 0)
        {
            if (trainCount < 20)
            {
                Train();
                trainCount++;
            }
            else
            {
                trainCount = 0;
                trainSpawn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            droneSpawn = true;
        }

        if (droneSpawn && Time.frameCount % 50 == 0)
        {
            if (droneCount < 20)
            {
                Drones();
                droneCount++;
            }
            else
            {
                droneCount = 0;
                droneSpawn = false;
            }
        }
    }

    private void Drones()
    {
        if(Time.frameCount % 100 == 0)
        {
            drone = Instantiate(dronePrefab);
            drone.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }

    private void Train()
    {

        var rand = new Vector3(0, Random.Range(-360, 360), 0);

        train = Instantiate(trainPrefab);
        train.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.Euler(rand));
        Destroy(train, 3f);

        hit = Instantiate(hitIndicator); 
        hit.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0,-4.2f,0), train.transform.rotation);
        hit.transform.localScale = new Vector3(2, 2, 30);
        Destroy(hit, 2f);
    }
}
