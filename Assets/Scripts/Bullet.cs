using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string damagableTag;
    [SerializeField] private LayerMask bulletLayer;
    [SerializeField] private float timeAlive;
    private string _from;


    private void Start()
    {
        StartCoroutine(IEDestroy());
    }

    private IEnumerator IEDestroy()
    {
        yield return new WaitForSeconds(timeAlive);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == damagableTag && !collision.isTrigger)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
        if (bulletLayer.Contains(collision.gameObject.layer))
        {
            if (_from == collision.gameObject.GetComponent<Bullet>().GetFrom())
                return;
        }

        if (collision.tag != "Undestroyable" && !collision.isTrigger)
            Destroy(gameObject);
    }

    public void SetFrom(string from)
    {
        _from = from;
    }

    public string GetFrom()
    {
        return _from;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
