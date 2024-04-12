using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class CollisionDamager : MonoBehaviour
{
    [SerializeField] private string damagableTag;
    [SerializeField] private float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == damagableTag && !collision.isTrigger)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

}
