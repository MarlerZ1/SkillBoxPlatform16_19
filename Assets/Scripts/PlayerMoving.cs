using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;


    private Rigidbody2D _rb;
    private Controls _controls;
    
    private void Awake()
    {
        _controls = ControlsSingletone.GetControls();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Move.Jumping.performed -= context => Jump();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Move.Jumping.performed += context => Jump();

    }


    private void Move()
    {
        float direction = _controls.Move.Moving.ReadValue<float>();
        print(direction);
        _rb.velocity = new Vector2(direction * moveSpeed, _rb.velocity.y);

    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce);
    }

    private void OnDestroy()
    {
        ControlsSingletone.Destroy(); 
    }
}
