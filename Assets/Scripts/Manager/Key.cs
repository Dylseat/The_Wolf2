using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Key : NetworkBehaviour
{
    PlayerController player;
    private int numberKeyValue = 7;
    [SerializeField]
    GameObject victoryUI;
    [SerializeField]
    Text keyNumber;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        victoryUI.gameObject.SetActive(false);
        setKeyText();
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {
        numberKeyValue--;
        setKeyText();
        if (numberKeyValue <= 0)
        {
            victoryUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        Destroy(gameObject);
    }

    void setKeyText()
    {
        keyNumber.text = "Number of Key " + numberKeyValue.ToString();
    }
}
