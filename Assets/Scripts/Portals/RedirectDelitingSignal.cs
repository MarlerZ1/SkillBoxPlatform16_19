using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectDelitingSignal : MonoBehaviour
{
    [SerializeField] private Portal portal;

    public void RedirectDestroySignal()
    {
        portal.DestroyGameObject();
    }
    
}
