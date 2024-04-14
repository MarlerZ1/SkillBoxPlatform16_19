using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class PlayerAnimationState : MonoBehaviour
{
    private Animator _animator;
    private string _currentState = "Idle";

    public string GetState()
    {
        return _currentState;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void HeroRun()
    {
        _currentState = "Run";
        _animator.SetBool("isRunning", true);
        _animator.SetBool("isAttack", false);
    }

    public void HeroAttackSword()
    {
        _currentState = "Attack";
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isAttack", true);
    }

    public void HeroIdle()
    {
        _currentState = "Idle";
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isAttack", false);
    }
    
    public void HeroAttackMagic()
    {
        _currentState = "AttackMagic";
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isAttack", false);
    }
}
