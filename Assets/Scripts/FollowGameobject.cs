using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameobject : MonoBehaviour
{
    [SerializeField] private Transform player;
   
    private void FixedUpdate()
    {
        if (player != null)
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
