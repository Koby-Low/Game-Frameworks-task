
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabAndThrow : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float throwForce = 8f;

    private GameObject nearbyObject;
    private GameObject heldObject;

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (heldObject == null)
        {
            PickUp();
        }
        else
        {
            Throw();
        }
    }

    void PickUp()
    {
        if (nearbyObject == null)
            return;

        heldObject = nearbyObject;
        nearbyObject = null;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Collider col = heldObject.GetComponent<Collider>();

        if (col != null)
            col.enabled = false;

        heldObject.transform.SetParent(holdPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
    }

    void Throw()
    {
        heldObject.transform.SetParent(null);

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        Collider col = heldObject.GetComponent<Collider>();

        if (col != null)
            col.enabled = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(
                transform.forward * throwForce,
                ForceMode.Impulse
            );
        }

        heldObject = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
            nearbyObject = other.gameObject;
        else if (other.CompareTag("Crop"))
            nearbyObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == nearbyObject)
            nearbyObject = null;
    }
}
