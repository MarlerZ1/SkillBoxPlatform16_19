using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Menu))]
public class GameWinMenu : MonoBehaviour
{
    [SerializeField] private Portal portal;
    [SerializeField] private TMP_Text text;
    private Menu _menu;
    private void Start()
    {
        _menu = GetComponent<Menu>();
        portal.OnLvlLastComplete += ActivateWinScreen;
    }


    private void ActivateWinScreen()
    {
        _menu.ChangePauseState();
        text.text = GameScoreCounter.GetGameScoreCounter().ToString();
    }
}
