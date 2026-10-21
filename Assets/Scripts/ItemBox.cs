using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private int itemsPlaced = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crop"))
        {
            itemsPlaced++;

            Debug.Log("Crops placed: " + itemsPlaced);

            Destroy(other.gameObject);
        }
    }

}
