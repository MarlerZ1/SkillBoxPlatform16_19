using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class DestroyByFall : MonoBehaviour
{
    [SerializeField] private string damagableTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == damagableTag)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(int.MaxValue);
        } else
        {
            Destroy(gameObject);
        }

    }
}
