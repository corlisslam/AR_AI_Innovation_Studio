using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoubleDeckerBus"))
        {
            Debug.Log("collision detected " + collision.gameObject.tag);
        }
        else if (collision.gameObject.CompareTag("PhoneBooth"))
        {
            Debug.Log("collision detected " + collision.gameObject.tag);
        }
        else if (collision.gameObject.CompareTag("TubeLogo"))
        {
            Debug.Log("collision detected " + collision.gameObject.tag);
        }
        else if (collision.gameObject.CompareTag("BigBen"))
        {
            Debug.Log("collision detected " + collision.gameObject.tag);
        }
        else if (collision.gameObject.CompareTag("Banksy"))
        {
            Debug.Log("collision detected " + collision.gameObject.tag);
        }
    }

}
