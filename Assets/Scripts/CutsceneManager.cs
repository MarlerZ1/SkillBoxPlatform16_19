using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private CinemachineBrain _cinemachineBrain;

    private void Awake()
    {
        _cinemachineBrain = GetComponent<CinemachineBrain>();
    }
}
