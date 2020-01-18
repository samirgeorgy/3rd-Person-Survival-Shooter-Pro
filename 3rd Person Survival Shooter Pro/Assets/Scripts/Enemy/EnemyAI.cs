using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region Private Variables

    /// <summary>
    /// Enemy States
    /// </summary>
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _gravity = 20.0f;
    [SerializeField] private float _attachDelay = 1.5f;
    [SerializeField] private EnemyState _currentState = EnemyState.Chase;

    private CharacterController _characterController;
    private Health _playerHealth;
    private Transform _player;
    private Vector3 _velocity;
    private float _nextAttack = -1;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();

        if (_player == null || _playerHealth == null)
            Debug.LogError("Player is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            _currentState = EnemyState.Attack;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            _currentState = EnemyState.Chase;
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Enemy Movement
    /// </summary>
    private void CalculateMovement()
    {
        if (_characterController.isGrounded)
        {
            Vector3 direction = _player.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);

            _velocity = direction * _speed;
        }
        else
            _velocity.y -= _gravity;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    /// <summary>
    /// Enemy Attack
    /// </summary>
    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
                _playerHealth.Damage(10);

            _nextAttack = Time.time + _attachDelay;
        }
    }

    #endregion
}
