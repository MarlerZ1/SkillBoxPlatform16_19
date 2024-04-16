using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private int addScore;
    [SerializeField] private LayerMask playerLayermask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayermask.Contains(addScore))
        {
            GameScoreCounter.IncreaseCount(addScore);
            Destroy(gameObject);
        }
    }
}
