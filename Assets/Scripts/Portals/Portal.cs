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
    public event Action OnLvlComplete;

    [Header("If player Collision waiting")]
    [SerializeField] private bool waitPlayerCollision;

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

    //revork
    public void DestroyGameObject()
    {
        bool isLvlLast = !_menu.CanLoadNextLvl();


        if (isLvlLast)
        {
            OnLvlLastComplete?.Invoke();
        } else
        {
            OnLvlComplete?.Invoke();
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (waitPlayerCollision && nextLvlActivateLayerMask.Contains(collision.gameObject.layer))
        {
            collision.gameObject.SetActive(false);
            animator.SetBool("isIdle", false);
        }
    }
}
