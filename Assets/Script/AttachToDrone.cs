using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToDrone : MonoBehaviour
{
    private Rigidbody rb;

    private Transform originalParent;
    [SerializeField] private Transform planeTransform;
    private bool isAttached = false;

    private void Start()
    {
        originalParent = transform.parent;
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        // Simulate attaching and detaching using a key press (e.g., "P").
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isAttached)
            {
                DetachFromPlane();
            }
            else
            {
                AttachToPlane();
            }
        }
    }

    private void AttachToPlane()
    {
        transform.SetParent(planeTransform); // Set the plane's transform as the parent.
        rb.isKinematic = true;
        isAttached = true;
    }

    private void DetachFromPlane()
    {
        transform.SetParent(originalParent); // Set the original parent as the parent.
        rb.isKinematic = false;
        isAttached = false;
    }
}
