using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1: MonoBehaviour
{
    public Transform playerHand,enemyHand;
    public GameManager manage_script;
    // Start is called before the first frame update
    void Start()
    {
        manage_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerHand = GameObject.Find("Player_hand").transform;
        enemyHand = GameObject.Find("Enemy_hand").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        
    }

    public void Draw()
    {
        manage_script.DrawCard(playerHand);
    }

    public void EnemyDraw()
    {
        manage_script.EnemyDraw(enemyHand);
    }
}
