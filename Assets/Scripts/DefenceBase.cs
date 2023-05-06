using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBase : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int hp;

    [SerializeField]
    private EnemyController enemyController;

    void Start()
    {
        hp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Mathf.Clamp(hp - enemyController.attackPoint, 0, maxHp);

            if (hp <= 0)
            {
                Debug.Log("Game Over...");
            }
        }
    }
}
