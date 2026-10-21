using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private float rotTime = 10f;
    [SerializeField] private Color rottenColor = Color.red;
    private bool isRotten = false;
    private Renderer cropRenderer;
    private Color originalColor;

    private float timer;

    void Start()
    {
        timer = rotTime;
        cropRenderer = GetComponent<Renderer>();

        if (cropRenderer != null)
        {
            originalColor = cropRenderer.material.color;
        }
    }

    void Update()
    {
        if (isRotten)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Rot();
        }
    }

    void Rot()
    {
        isRotten = true;

        if (cropRenderer != null)
        {
            cropRenderer.material.color = rottenColor;
        }

        Debug.Log(gameObject.name + " has rotted!");
    }

}