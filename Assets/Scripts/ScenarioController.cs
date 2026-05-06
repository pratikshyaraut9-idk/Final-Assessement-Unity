using UnityEngine;
using UnityEngine.Events;

public class ScenarioController : MonoBehaviour
{
    public static ScenarioController Instance { get; private set; }

    public UnityEvent<ScenarioData> OnScenarioTriggered;
    public UnityEvent OnScenarioCompleted;

    [Header("SFX")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _safeClip;
    [SerializeField] private AudioClip _riskyClip;
    
    [Header("Ambient")]
    [SerializeField] private AudioSource _heartbeatSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        if (_sfxSource == null) _sfxSource = gameObject.AddComponent<AudioSource>();
        if (_heartbeatSource != null) _heartbeatSource.Stop();
    }

    public void TriggerScenario(ScenarioData data)
    {
        Debug.Log($"Scenario Triggered: {data.ScenarioTitle}");
        OnScenarioTriggered?.Invoke(data);
        
        if (_heartbeatSource != null) _heartbeatSource.Play();
        
        Time.timeScale = 0; 
    }

    public void CompleteScenario(bool isSafe, string nextScene)
    {
        Debug.Log($"Scenario Completed. Safe: {isSafe}");
        
        if (isSafe)
        {
            GameManager.Instance.AddScore(1);
            if (_safeClip != null) _sfxSource.PlayOneShot(_safeClip);
        }
        else
        {
            if (_riskyClip != null) _sfxSource.PlayOneShot(_riskyClip);
        }
        
        if (_heartbeatSource != null) _heartbeatSource.Stop();

        Time.timeScale = 1;
        OnScenarioCompleted?.Invoke();
        
        if (!string.IsNullOrEmpty(nextScene))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}
