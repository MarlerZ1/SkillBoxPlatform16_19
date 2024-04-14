using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyPatrol : MonoBehaviour
{
    public event Action<float> OnRotationChanged;


    [Header("RayCast settings")]
    [SerializeField] private float rayCastDistance;
    [SerializeField] private Transform groundRaycastPointFirst;
    [SerializeField] private Transform groundRaycastPointSecond;
    [SerializeField] private LayerMask layerMask;


    [Header("Moving settings")]
    [SerializeField] private float speed;
    [SerializeField] private float timeToIdleState;

    private int _direction = 1;
    private Rigidbody2D _rb;
    private Animator _animator;
    private string _enemyState = "Running";


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }
    private void Start()
    {
        EnemyWalking();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(groundRaycastPointFirst.position, Vector3.down * rayCastDistance, Color.yellow);
        Debug.DrawRay(groundRaycastPointSecond.position, Vector3.down * rayCastDistance, Color.red);


        _rb.velocity = new Vector2(_direction * speed, _rb.velocity.y);
        //print(_rb.velocity + " col: " +!Physics2D.Raycast(groundRaycastPointFirst.position, Vector2.down, rayCastDistance, layerMask) + " " + !Physics2D.Raycast(groundRaycastPointSecond.position, Vector2.down, rayCastDistance, layerMask));
    
        if (_enemyState != "Idle" && !Physics2D.Raycast(groundRaycastPointFirst.position, Vector2.down, rayCastDistance, layerMask) &&
            !Physics2D.Raycast(groundRaycastPointSecond.position, Vector2.down, rayCastDistance, layerMask))
        {
            StartCoroutine(IEPlayIdleAnimation());
        }
    }

    private IEnumerator IEPlayIdleAnimation()
    {
        EnemyIdle();
        int currentDirection = _direction;
        _direction = 0;

        yield return new WaitForSeconds(timeToIdleState);
        _direction = currentDirection;
        EnemyWalking();
        ChangeDirection();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerMask.Contains(collision.gameObject.layer))
        {
            StartCoroutine(IEPlayIdleAnimation());
        }
    }


    private void ChangeDirection()
    {
        _direction = -_direction;
        //print("Enemy rot1" + transform.rotation.y);
        bool rotationFlag = transform.rotation.y == -1;
        transform.rotation = Quaternion.Euler(0, rotationFlag ? 0 : 180, 0);
        OnRotationChanged?.Invoke(rotationFlag ? 180 : 0);
        //print("Enemy rot2" + transform.rotation.y);
    }

    private void EnemyIdle()
    {
        _enemyState = "Idle";
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isWalking", false);
    }

    private void EnemyWalking()
    {
        _enemyState = "Running";
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isWalking", true);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
