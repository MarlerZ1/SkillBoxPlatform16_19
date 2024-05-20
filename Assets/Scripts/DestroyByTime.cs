using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float time;

    private void Awake()
    {
        StartCoroutine(IEDestroy());
    }

    private IEnumerator IEDestroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
