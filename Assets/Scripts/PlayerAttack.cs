using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerAnimationState))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject magicAttackObject;
    [SerializeField] private Transform magicAttackStartPoint;

    [Header("Magic attack balance settings")]
    [SerializeField] private float magicAttackSpeed;
    [SerializeField] private float coldownTime;

    private PlayerAnimationState _playerAnimationState;
    private Controls _controls;
    private bool _canUseMagicAttack = true;
    //private SpriteRenderer _sp;

    private void MagicAttack()
    {

        if (_canUseMagicAttack)
        {
            StartCoroutine(IEMagicAttackColdown());
            _playerAnimationState.HeroAttackMagic();



            GameObject magicBullet = Instantiate(magicAttackObject, magicAttackStartPoint.position, Quaternion.Euler(0, transform.rotation.y == -1 ? 180 : 0, 0));
            magicBullet.GetComponent<Bullet>().SetFrom("Player");

            Rigidbody2D magicBulletRb = magicBullet.GetComponent<Rigidbody2D>();

            //int direction = _sp.flipX ? -1 : 1;


            int direction = transform.rotation.y == -1 ? -1 : 1;
            magicBulletRb.velocity = new Vector2(magicAttackSpeed * direction, magicBulletRb.velocity.y);
        }
    }

    private IEnumerator IEMagicAttackColdown()
    {
        _canUseMagicAttack = false;
        yield return new WaitForSeconds(coldownTime);
        _canUseMagicAttack = true;
    }


    private void Awake()
    {
        _controls = ControlsSingletone.GetControls();
        _playerAnimationState = GetComponent<PlayerAnimationState>();
        //_sp = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Attack.Magic.performed -= context => MagicAttack();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Attack.Magic.performed += context => MagicAttack();

    }


    private void OnDestroy()
    {
        StopAllCoroutines();
        ControlsSingletone.Destroy();
    }
}
