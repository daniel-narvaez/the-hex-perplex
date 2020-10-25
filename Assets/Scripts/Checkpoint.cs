using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    bool pointActivated = false;
    public static GameObject startingPoint;
    public GameObject checkpointLight;
    
    public static GameObject[] checkpointsList;
    void Start()
    {
        startingPoint = GameObject.Find("StartingPoint");
        checkpointsList = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointLight.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(pointActivated == true)
        {
            checkpointLight.SetActive(true);
        } else
        {
            checkpointLight.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActivateCheckpoint();
        }
    }

    void ActivateCheckpoint()
    {
        foreach (GameObject point in checkpointsList)
        {
            point.GetComponent<Checkpoint>().pointActivated = false;
        }
        pointActivated = true;
    }

    //needs to be called from the player script
    public static Vector2 CurrentlyActiveCheckpoint()
    {
        Vector2 newPoint = startingPoint.transform.position;
        if (checkpointsList != null)
        {
            foreach (GameObject point in checkpointsList)
            {
                if(point.GetComponent<Checkpoint>().pointActivated)
                {
                    newPoint = point.transform.position;
                    break;
                }
            }
        }
        return newPoint;
    }
}
