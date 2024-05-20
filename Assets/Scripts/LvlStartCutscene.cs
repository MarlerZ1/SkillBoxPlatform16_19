using Cinemachine;
using System.Collections;
using UnityEditor;
using UnityEngine;


[DisallowMultipleComponent]
public class LvlStartCutscene : MonoBehaviour
{
    [SerializeField] private float timeToSpawnPlayer;
    [SerializeField] private float animTime;
    [SerializeField] private float timeBeforAnim;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMoving playerMoving;
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private CinemachineVirtualCamera cmEnd; 
    [SerializeField] private CinemachineVirtualCamera cmPlayer;


    [SerializeField] private GameObject BlackLines;
    private void Awake()
    {
        StartCoroutine(IEPlayCutscene());
    }

    private IEnumerator IEPlayCutscene()
    {
        yield return new WaitForSeconds(timeToSpawnPlayer);
        player.SetActive(true);
        cmPlayer.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBeforAnim);

        cmEnd.gameObject.SetActive(true);
        yield return new WaitForSeconds(animTime);
        cmEnd.gameObject.SetActive(false);
        yield return new WaitForSeconds(animTime);
        playerMoving.enabled = true;
        playerAttack.enabled = true;
        BlackLines.SetActive(false);
    }
}
