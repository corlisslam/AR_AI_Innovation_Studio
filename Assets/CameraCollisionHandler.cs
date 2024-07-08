
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    //public string otherColliderObjectTag;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CollisionHandler script started on " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter called on " + gameObject.name);

        if (collision.gameObject.CompareTag("SpawnedPrefab")) // or is it .tag?
        {
            Debug.Log("Camera collided with spwaned prefab, July 8"); // just testing it out with bus right now, but should have a separate script called for every object
        }
        else
        {
            Debug.Log("Collision with non-camera object: " + collision.gameObject.name);
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    //public string otherColliderObjectTag;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CollisionHandler script started on " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter called on " + gameObject.name);

        if (other.gameObject.CompareTag("SpawnedPrefab")) // or is it .tag?
        {
            Debug.Log("Camera collided with an object"); // just testing it out with bus right now, but should have a separate script called for every object
        }
        else
        {
            Debug.Log("Collision with non-camera object: " + other.gameObject.name);
        }
    }
}
*/