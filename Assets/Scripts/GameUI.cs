using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private Label cropsLabel;
    private Label scoreLabel;
    private int totalCropsPlaced = 0;
    private int totalScore = 0;

    void Awake()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        cropsLabel = root.Q<Label>("crops-text-label");
        scoreLabel = root.Q<Label>("score-text-label");

        UpdateDisplay();
    }
    public void UpdateCropsPlaced(int cropsPlaced)
    {
        totalCropsPlaced = cropsPlaced;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (cropsLabel != null)
            cropsLabel.text = "Crops placed: " + totalCropsPlaced;

        if (scoreLabel != null)
            scoreLabel.text = "Score: " + totalScore;
    }
    public void AddScore(int points)
    {
        totalScore += points;
        UpdateDisplay();
    }
    public void RemoveScore(int points)
    {
        totalScore -= points;

        if (totalScore < 0)
        {
            totalScore = 0;
        }

        UpdateDisplay();
    }
}
