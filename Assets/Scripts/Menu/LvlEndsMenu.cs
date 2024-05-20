using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[DisallowMultipleComponent]
[RequireComponent(typeof(Menu))]
public class LvlEndsMenu : MonoBehaviour
{
    [SerializeField] private Portal portal;
    [SerializeField] private TMP_Text text;
    private Menu _menu;
    private void Start()
    {
        _menu = GetComponent<Menu>();
    }

    private void OnEnable()
    {
        portal.OnLvlComplete += ActivateLvlEndScreen;
    }

    private void OnDisable()
    {
        portal.OnLvlComplete -= ActivateLvlEndScreen;
    }

    private void ActivateLvlEndScreen()
    {
        _menu.ChangePauseState();
        text.text = GameScoreCounter.GetGameScoreCounter().ToString();
    }
}
