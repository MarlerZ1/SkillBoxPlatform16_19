using System.Collections;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private GameObject boom;
    [SerializeField] private float timeBefore;
    private void Awake()
    {
        StartCoroutine(IESpawn());
    }

    private IEnumerator IESpawn()
    {
        yield return new WaitForSeconds(timeBefore);
        Instantiate(boom, transform.position, Quaternion.identity);
    }
}
