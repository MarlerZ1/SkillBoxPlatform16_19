using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Menu))]
public class PauseMenu : MonoBehaviour
{
    private Menu _menu;
    private Controls _controls;

    private void Awake()
    {
        _controls = ControlsSingletone.GetControls();
        _menu = GetComponent<Menu>();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.UI.Pause.performed += context => _menu.ChangePauseState();
    }


    private void OnDisable()
    {
        _controls.UI.Pause.performed -= context => _menu.ChangePauseState();
        _controls.Disable();
    }
}
