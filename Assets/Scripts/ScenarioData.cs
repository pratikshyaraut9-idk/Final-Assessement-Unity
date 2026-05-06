using UnityEngine;

[CreateAssetMenu(fileName = "NewScenario", menuName = "Game/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string ScenarioTitle;
    [TextArea] public string Description;
    
    [Header("Choice A")]
    public string ChoiceAText;
    [TextArea] public string FeedbackA;
    public bool IsSafeA;
    
    [Header("Choice B")]
    public string ChoiceBText;
    [TextArea] public string FeedbackB;
    public bool IsSafeB;

    public string NextSceneName;
}
