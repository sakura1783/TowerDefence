using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBase : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int hp;

    private GameManager gameManager;

    private UIManager uiManager;

    [SerializeField] private LifeGauge lifeGauge;

    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="hp"></param>
    /// <param name="uiManager"></param>
    public void SetUpDefenceBase(GameManager gameManager, int hp, UIManager uiManager)
    {
        this.gameManager = gameManager;
        Debug.Log("this.gameManager のインスタンス番号 :" + this.gameManager.GetInstanceID());
        Debug.Log("DefenceBase のインスタンス番号 :" + this.GetInstanceID());
        this.uiManager = uiManager;

        if (GameData.instance.isDebug)
        {
            maxHp = GameData.instance.defenceBaseLife;
        }

        this.hp = maxHp;
    }

    //TODO 設定用メソッドの作成。作成後はStartメソッドを削除

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerが動きます" + collision.gameObject.name);

        //Debug.Log("DefenceBase のインスタンス番号 :" + this.GetInstanceID());
        //Debug.Log("this.gameManager のインスタンス番号 :" + this.gameManager.GetInstanceID());

        Debug.Log(collision.gameObject);
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            Debug.Log("OnTrigger内のif文が動きます");
            hp =  Mathf.Clamp(hp - enemyController.attackPoint, 0, maxHp);

            //ダメージ演出生成
            CreateDamageEffect();

            //ゲーム画面に耐久力の表示がある場合、その表示を更新
            lifeGauge.ReduceLifeGauge();

            if (hp <= 0 && gameManager.currentGameState == GameManager.GameState.Play)
            {
                Debug.Log("Game Over...");

                //TODO ゲームオーバー処理
            }

            enemyController.DestroyEnemy();
        }
    }

    private void CreateDamageEffect()
    {
        GameObject effect = Instantiate(BattleEffectManager.instance.GetEffect(EffectType.Hit_DefenceBase), transform.position, Quaternion.identity);

        Destroy(effect, 1.5f);
    }
}