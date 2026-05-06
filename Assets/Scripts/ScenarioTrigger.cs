using UnityEngine;

public class ScenarioTrigger : MonoBehaviour
{
    [SerializeField] private ScenarioData _scenarioData;
    private bool _triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        
        if (other.CompareTag("Player"))
        {
            _triggered = true;
            ScenarioController.Instance.TriggerScenario(_scenarioData);
        }
    }
}
