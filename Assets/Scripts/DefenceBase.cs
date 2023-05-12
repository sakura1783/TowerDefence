using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBase : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int hp;

    void Start()
    {
        if (GameData.instance.isDebug)
        {
            maxHp = GameData.instance.defenceBaseLife;
        }
        else
        {
            maxHp = hp;
        }
    }

    //TODO 設定用メソッドの作成。作成後はStartメソッドを削除

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            hp =  Mathf.Clamp(hp - enemyController.attackPoint, 0, maxHp);

            //TODO ダメージ演出生成

            //TODO ゲーム画面に耐久力の表示がある場合、その表示を更新

            if (hp <= 0)
            {
                Debug.Log("Game Over...");

                //TODO ゲームオーバー処理
            }

            enemyController.DestroyEnemy();
        }
    }

    //TODO ダメージ演出生成用のメソッドの作成
}
