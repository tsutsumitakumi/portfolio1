using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CoinT : MonoBehaviourPunCallbacks
{
    private Animator CoinAnim;
    public Server Server;
    public Button CoinPanel;
    private bool Click;

    public int Coin;

    // Start is called before the first frame update
    void Start()
    {
        CoinAnim = GetComponent<Animator>();
        Server = GameObject.Find("Server").GetComponent<Server>();
        CoinPanel = GameObject.Find("CoinPanel").GetComponent<Button>();
        Click = false;
    }

    // Update is called once per frame
    void Update()
    {
        CoinToss();
    }

    public void OnClick()
    {
        Click = true;
    }
    void CoinToss()
    {
        if (Server.OnServer == true)
        {
            if (Click == true && PhotonNetwork.IsMasterClient)
            {
                Coin = Random.Range(0, 2);
                Debug.Log(Coin + "‚Å‚·");
                CoinAnim.SetInteger("CoinCheck", Coin);
            }
        }
        else
        {
            if (Click == true)
            {
                Coin = 0;
                CoinAnim.SetInteger("CoinCheck", Coin);
            }
        }
    }

    public void OnAnimationEnd()
    {
        if(Server.OnServer == true)
        {
            PhotonNetwork.LoadLevel("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
