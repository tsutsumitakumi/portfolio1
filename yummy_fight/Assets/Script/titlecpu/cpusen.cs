using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpusen : MonoBehaviour
{
    public GameObject Coin;
    public GameObject StartPanel;
    public GameObject LoginPanel;


    // Start is called before the first frame update
    void Start()
    {
        StartPanel.SetActive(true);
        LoginPanel.SetActive(false);
        Coin.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CPUButton()
    {
        StartPanel.SetActive(false);
        Coin.SetActive(true);

    }

    void PvPButton()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(true);

    }
}
