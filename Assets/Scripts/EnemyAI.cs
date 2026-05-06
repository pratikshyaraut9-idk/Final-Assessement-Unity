using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 15f;
    [SerializeField] private float _stopFollowingDistance = 20f;
    
    private Transform _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isFollowing = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        
        // Find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _player = playerObj.transform;
    }

    private void Update()
    {
        if (_player == null) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (!_isFollowing && distance < _detectionRange)
        {
            _isFollowing = true;
        }
        else if (_isFollowing && distance > _stopFollowingDistance)
        {
            _isFollowing = false;
            _agent.ResetPath();
        }

        if (_isFollowing)
        {
            _agent.SetDestination(_player.position);
        }

        if (_animator != null)
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EliminatePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EliminatePlayer();
        }
    }

    private void EliminatePlayer()
    {
        Debug.Log("Player Eliminated!");
        // Reset current scene or go to a Game Over screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
