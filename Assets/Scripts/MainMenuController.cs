using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _mainMenu;
    private VisualElement _settingsMenu;

    private Button _startButton;
    private Button _settingsButton;
    private Button _exitButton;
    private Button _backButton;

    private Toggle _muteToggle;
    private DropdownField _graphicsDropdown;

    private AudioSource _bgmSource;

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        _bgmSource = GetComponent<AudioSource>();

        if (_uiDocument == null) return;

        var root = _uiDocument.rootVisualElement;
        if (root == null) return;

        _mainMenu = root.Q<VisualElement>("mainMenu");
        _settingsMenu = root.Q<VisualElement>("settingsMenu");

        _startButton = root.Q<Button>("startButton");
        _settingsButton = root.Q<Button>("settingsButton");
        _exitButton = root.Q<Button>("exitButton");
        _backButton = root.Q<Button>("backButton");

        _muteToggle = root.Q<Toggle>("muteToggle");
        _graphicsDropdown = root.Q<DropdownField>("graphicsDropdown");

        // Set up buttons with null checks
        if (_startButton != null) _startButton.clicked += OnStartClicked;
        if (_settingsButton != null) _settingsButton.clicked += OnSettingsClicked;
        if (_exitButton != null) _exitButton.clicked += OnExitClicked;
        if (_backButton != null) _backButton.clicked += OnBackClicked;

        // Set up settings
        SetupSettings();
    }

    private void OnDisable()
    {
        if (_startButton != null) _startButton.clicked -= OnStartClicked;
        if (_settingsButton != null) _settingsButton.clicked -= OnSettingsClicked;
        if (_exitButton != null) _exitButton.clicked -= OnExitClicked;
        if (_backButton != null) _backButton.clicked -= OnBackClicked;
    }

    private void OnStartClicked()
    {
        Debug.Log("Start Clicked");
        SceneManager.LoadScene("NightclubExit");
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings Clicked");
        if (_mainMenu != null) _mainMenu.style.display = DisplayStyle.None;
        if (_settingsMenu != null) _settingsMenu.style.display = DisplayStyle.Flex;
    }

    private void OnBackClicked()
    {
        Debug.Log("Back Clicked");
        if (_mainMenu != null) _mainMenu.style.display = DisplayStyle.Flex;
        if (_settingsMenu != null) _settingsMenu.style.display = DisplayStyle.None;
    }

    private void OnExitClicked()
    {
        Debug.Log("Exit Clicked");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetupSettings()
    {
        if (_muteToggle != null)
        {
            _muteToggle.value = PlayerPrefs.GetInt("Muted", 0) == 1;
            ApplyMute(_muteToggle.value);
            _muteToggle.RegisterValueChangedCallback(evt => {
                ApplyMute(evt.newValue);
                PlayerPrefs.SetInt("Muted", evt.newValue ? 1 : 0);
            });
        }

        if (_graphicsDropdown != null)
        {
            List<string> options = new List<string>(QualitySettings.names);
            _graphicsDropdown.choices = options;
            _graphicsDropdown.index = QualitySettings.GetQualityLevel();
            _graphicsDropdown.RegisterValueChangedCallback(evt => {
                int index = _graphicsDropdown.choices.IndexOf(evt.newValue);
                if (index >= 0)
                {
                    QualitySettings.SetQualityLevel(index);
                    PlayerPrefs.SetInt("QualityLevel", index);
                }
            });
        }
    }

    private void ApplyMute(bool isMuted)
    {
        if (_bgmSource != null)
        {
            _bgmSource.mute = isMuted;
        }
    }
}
