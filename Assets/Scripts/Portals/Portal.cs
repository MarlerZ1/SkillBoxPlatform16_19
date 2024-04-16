using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Menu))]
public class Portal : MonoBehaviour
{

    public event Action OnLvlLastComplete;

    [Header("If player Collision waiting")]
    [SerializeField] private bool waitPlayerCollision;
    [SerializeField, Range(0, 120)] private float animationCloseWait = 3f;

    [Header("If just exists")]
    [SerializeField, Range(0, 120)] private float existsTime;

    [Header("Objects")]
    [SerializeField] private LayerMask nextLvlActivateLayerMask;
    [SerializeField] private Animator animator;

    private Menu _menu;

    private void Awake()
    {
        _menu = GetComponent<Menu>();
    }

    private void Start()
    {
        animator.SetBool("isIdle", true);

        if (!waitPlayerCollision)
            StartCoroutine(IEWaitForClouse());
    }



    private IEnumerator IEWaitForClouse()
    {
        yield return new WaitForSeconds(existsTime);
        animator.SetBool("isIdle", false);
    }


    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (nextLvlActivateLayerMask.Contains(collision.gameObject.layer))
        {
            StartCoroutine(IEWaitForDelete());
        }
    }


    private IEnumerator IEWaitForDelete()
    {
        animator.SetBool("isIdle", false);
        yield return new WaitForSeconds(animationCloseWait);

        bool isLvlLast = !_menu.LoadNextLvlBool();
        
        if (isLvlLast)
        {
            OnLvlLastComplete?.Invoke();
        }
    }
}
