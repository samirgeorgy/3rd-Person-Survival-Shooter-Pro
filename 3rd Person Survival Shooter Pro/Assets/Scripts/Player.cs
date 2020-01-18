using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    [Header("Controller Settings")]
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = 20.0f;
    [SerializeField] private float _jumpHeight = 8.0f;

    [Header("Camera Settings")]
    [SerializeField] private float _cameraSensitivity = 1;

    private CharacterController _characterController;
    private Camera _playerCamera;

    private Vector3 _direction;
    private Vector3 _velocity;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;

        if (_characterController == null)
            Debug.LogError("Character Controller is NULL");

        if (_playerCamera == null)
            Debug.LogError("Main Camera is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Moves the player
    /// </summary>
    private void Movement()
    {
        if (_characterController.isGrounded)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontalMovement, 0, verticalMovement);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
                _velocity.y = _jumpHeight;
            
        }

        _velocity.y -= _gravity * Time.deltaTime;

        _velocity = transform.TransformDirection(_velocity);

        _characterController.Move(_velocity * Time.deltaTime);
    }

    /// <summary>
    /// Controls the third person camera
    /// </summary>
    private void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //Look Up and Down
        Vector3 currentCameraRotation = _playerCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _cameraSensitivity;
        _playerCamera.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    #endregion
}
