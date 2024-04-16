using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Menu))]
public class GameWinMenu : MonoBehaviour
{
    [SerializeField] private Portal portal;

    private Menu _menu;
    private void Start()
    {
        _menu = GetComponent<Menu>();
        portal.OnLvlLastComplete += ActivateWinScreen;
    }


    private void ActivateWinScreen()
    {
        _menu.ChangePauseState();
    }
}
