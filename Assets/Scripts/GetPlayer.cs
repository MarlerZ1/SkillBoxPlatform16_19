using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayer : MonoBehaviour
{
    private static GameObject _player;

    private void Awake()
    {
        _player = gameObject;
    }

    public static GameObject GetPlayerObject()
    {
        return _player;
    }
}
