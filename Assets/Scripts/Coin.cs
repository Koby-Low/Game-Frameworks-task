using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreValue = 50;

    public int GetScoreValue()
    {
        return scoreValue;
    }

}
