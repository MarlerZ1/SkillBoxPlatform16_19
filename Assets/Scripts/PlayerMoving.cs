using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerAnimationState))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoving : MonoBehaviour
{

    [Header("Move force")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMask; 

    [Header("Move Curve Settings")]
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private int interpolationFramesCount;

    private bool _isGrounded = false;
    private int elapsedFrames = 0;
    private PlayerAnimationState _playerAnimationState;
    private Rigidbody2D _rb;
    private Controls _controls;

    private void Awake()
    {
        _controls = ControlsSingletone.GetControls();
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimationState = GetComponent<PlayerAnimationState>();
        //_sp = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
        //Debug.DrawRay(rayCastObj.position, Vector3.down * raycastDistance, Color.yellow);
    }


    private void Move()
    {

        float direction = _controls.Move.Moving.ReadValue<float>();
        if (direction == 1)
        {
            _playerAnimationState.HeroRun();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (direction == -1)
        {
            _playerAnimationState.HeroRun();
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (direction == 0)
        {
            elapsedFrames = 0;
            if (_playerAnimationState.GetState() == "Run")
                _playerAnimationState.HeroIdle();
        }
        else
            elapsedFrames++;


        float interpolatedValue = Mathf.Lerp(0, direction, (float)elapsedFrames / interpolationFramesCount);
        _rb.velocity = new Vector2(curve.Evaluate(interpolatedValue) * moveSpeed, _rb.velocity.y);
       // print(_rb.velocity);

    }

    private void Jump()
    {
        
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce);
            _isGrounded = false;
        }
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

    private void OnDestroy()
    {
        ControlsSingletone.Destroy(); 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (layerMask.Contains(collision.gameObject.layer))
        {
            //print("_isGrounded " + _isGrounded);
            _isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (layerMask.Contains(collision.gameObject.layer))
        {
            //print("_isGrounded " + _isGrounded);
            _isGrounded = false;
        }
    } 

   
}
