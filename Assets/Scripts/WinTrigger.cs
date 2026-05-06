using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Reached home safely!");
            // Trigger the final outcome scenario or scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("FinalOutcome");
        }
    }
}
