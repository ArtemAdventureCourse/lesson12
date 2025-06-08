using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _moveLeft;
    [SerializeField] private Vector3 _moveRight;
    [SerializeField] private Vector3 _moveForward;
    [SerializeField] private Vector3 _moveBack;
    [SerializeField] private readonly float _moveSpeed = 12f;
    [SerializeField] private Transform _camera; 

    private float _score;
    private Vector3 _jumpPower = new Vector3(0, 2, 0);
    private Vector3 _camForward;
    private Vector3 _camBack;
    private Vector3 _camRight;

    private bool PressAKey => Input.GetKeyDown(KeyCode.A);
    private bool PressDKey => Input.GetKeyDown(KeyCode.D);
    private bool PressWKey => Input.GetKeyDown(KeyCode.W);
    private bool PressSKey => Input.GetKeyDown(KeyCode.S);
    private bool PressSpaceKey => Input.GetKeyDown(KeyCode.Space);
    private bool PressYKey => Input.GetKeyDown(KeyCode.Y);

    private void Update()
    {
        Jump();
        Move();
        SetCamera();
    }

    private void SetCamera()
    {
        _camForward = _camera.forward;
        _camForward.y = 0;
        _camForward.Normalize();

        _camRight = _camera.right;
        _camRight.y = 0;
        _camRight.Normalize();
    }

    private void Jump()
    {
        if (PressSpaceKey)
         ApplyForceForMove(_jumpPower); 
    }

    private void Move()
    {
        if (PressAKey)
            ApplyForceForMove(_moveLeft.normalized);
        else if (PressDKey)
            ApplyForceForMove((_camRight.normalized));
        else if (PressWKey)
            ApplyForceForMove((_camForward.normalized));
        else if (PressSKey)
            ApplyForceForMove((-_camForward.normalized));
    }

    private void ApplyForceForMove(Vector3 direction) => _rigidbody.AddForce(direction * _moveSpeed, ForceMode.Impulse);

}