using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]

public class EnemyHealthVisualization : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;
    [SerializeField] private GameObject hpBarGameObject;
    [SerializeField] private Image hpBarImage;
    [SerializeField] private EnemyPatrol enemyPatrol;


    private void OnEnable()
    {
       //transform.localScale = new Vector2(100, 100);
        enemyPatrol.OnRotationChanged += ChangeRotation;
        enemyHealth.OnHealthChanged += HPBarValueChanger;
    }

    private void OnDisable()
    {
        enemyPatrol.OnRotationChanged -= ChangeRotation;
        enemyHealth.OnHealthChanged += HPBarValueChanger;
    }

    private void ChangeRotation(float rotation)
    {
        transform.Rotate(new Vector3(0, 180, 0), Space.World);
        //transform.rotation = Quaternion.Euler(0, rotation * 2, 0);
    }


    private void HPBarValueChanger(float damage, float maxHp, float currentHp)
    {
        
        if (currentHp != maxHp && !hpBarGameObject.activeInHierarchy)
        {
            hpBarGameObject.SetActive(true);
            hpBarImage.fillAmount = (float)currentHp / maxHp;
           
        }


    }
}
