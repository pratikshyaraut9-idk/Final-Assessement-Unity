using UnityEngine;
using UnityEngine.UIElements;

public class FinalOutcomeController : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        var scoreText = root.Q<Label>("scoreText");
        var messageText = root.Q<Label>("messageText");
        var restartButton = root.Q<Button>("restartButton");

        int score = GameManager.Instance != null ? GameManager.Instance.Score : 0;
        scoreText.text = $"Safety Score: {score}/3";

        if (score == 3) {
            messageText.text = "Excellent! You made all the safe choices. You have a great awareness of night safety.";
        } else if (score >= 1) {
            messageText.text = "Good job getting home, but some choices were risky. Remember to prioritize well-lit areas and avoid strangers.";
        } else {
            messageText.text = "You made it home, but your choices were very risky. Try to be more aware of your surroundings next time.";
        }

        restartButton.clicked += () => {
            if (GameManager.Instance != null) GameManager.Instance.ResetGame();
            else UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        };
    }
}
