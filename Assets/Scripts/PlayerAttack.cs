using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerAnimationState))]
public class PlayerAttack : MonoBehaviour
{

    [Header("Attack support objects")]
    [SerializeField] private GameObject magicAttackObject;
    [SerializeField] private Transform magicAttackStartPoint;
    [SerializeField] private Transform swordAttackCollisionPoint;

    [Header("Support info")]
    [SerializeField] private string damagableTag;
    [SerializeField] private LayerMask dontDamagableLayers;

    [Header("Magic attack balance settings")]
    [SerializeField] private float magicAttackSpeed;
    [SerializeField] private float magicAttackColdownTime;

    [Header("Sword attack balance settings")]
    [SerializeField] private float swordAttackDamage;
    [SerializeField] private float swordAttackDamageDelay;
    [SerializeField] private float swordAttackColdownTime;
    [SerializeField] private float swordAttackRadius;

   

    private PlayerAnimationState _playerAnimationState;
    private Controls _controls;
    private bool _canUseMagicAttack = true;
    private bool _canUseSwordAttack = true;
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

    private void SwordAttack()
    {
        if (_canUseSwordAttack)
        {
            _playerAnimationState.HeroAttackSword();
            StartCoroutine(IESwordAttackColdown());
        }
            
    }

    private IEnumerator IEMagicAttackColdown()
    {
        _canUseMagicAttack = false;
        yield return new WaitForSeconds(magicAttackColdownTime);
        _canUseMagicAttack = true;
    }


    private IEnumerator IESwordAttackColdown()
    {
        _canUseSwordAttack = false;

        yield return new WaitForSeconds(swordAttackDamageDelay);
        Collider2D[] others = Physics2D.OverlapCircleAll(swordAttackCollisionPoint.position, swordAttackRadius);


        if (others.Length == 0)
            yield return null;

        print("Layers: " + others.Length);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].gameObject.tag == damagableTag && !others[i].isTrigger && !dontDamagableLayers.Contains(others[i].gameObject.layer))
            {
                others[i].GetComponent<Health>().TakeDamage(swordAttackDamage);
                print("Damage " + swordAttackDamage);
            }
        }
    



        yield return new WaitForSeconds(swordAttackColdownTime);

        _canUseSwordAttack = true;
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
        _controls.Attack.Sword.performed -= context => SwordAttack();
        _controls.Attack.Magic.performed -= context => MagicAttack();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Attack.Sword.performed += context => SwordAttack();
        _controls.Attack.Magic.performed += context => MagicAttack();

    }


    private void OnDestroy()
    {
        StopAllCoroutines();
        ControlsSingletone.Destroy();
    }
}
