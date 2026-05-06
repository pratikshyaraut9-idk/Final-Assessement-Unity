using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _score = 0;
    public int Score => _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        _score += points;
        Debug.Log($"Score updated: {_score}");
    }

    public void ResetGame()
    {
        _score = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
