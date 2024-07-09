/*
using UnityEngine;

public class CameraRigFollower : MonoBehaviour
{
    public Camera arCamera; // Reference to the AR camera's transform
    private Rigidbody rb;
    //private Vector3 offset;
 

    void Start()
    {
        //arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("AR Camera is not assigned.");
            return;
        }

        rb = GetComponent<Rigidbody>();
   
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; // Disable gravity if not needed
        }

        

        // Calculate the initial offset between the parent and the AR camera
        //offset = transform.position - arCamera.position;
    }

    void FixedUpdate()
    {
        // Sync the parent GameObject's position and rotation with the AR camera
        rb.MovePosition(arCamera.transform.position);
        rb.MoveRotation(arCamera.transform.rotation);
        Debug.Log($"CameraRigParent Position: {transform.position}, Rotation: {transform.rotation.eulerAngles}");
        Debug.Log($"AR Camera Position: {arCamera.transform.position}, Rotation: {arCamera.transform.rotation.eulerAngles}");
        //Debug.Log("Main camera moved so parent empty object moved");
    }

}
*/

