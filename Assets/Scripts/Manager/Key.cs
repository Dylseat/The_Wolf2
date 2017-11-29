using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Key : NetworkBehaviour
{
    PlayerController player;
    private int numberSwitchValue = 7;

    [SerializeField]
    GameObject victoryUI;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        victoryUI.gameObject.SetActive(false);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {
        numberSwitchValue--;
        if (numberSwitchValue <= 0)
        {
            victoryUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        Destroy(gameObject);
    }
}
