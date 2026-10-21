using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private GameUI gameUI;
    private float timer = 0f;

    void Awake()
    {
        gameUI = FindAnyObjectByType<GameUI>();
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        timer += Time.fixedDeltaTime;

        if (timer >= 1f)
        {
            if (gameUI != null)
            {
                gameUI.RemoveScore(1);
            }

            timer -= 1f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
        }
    }
}
