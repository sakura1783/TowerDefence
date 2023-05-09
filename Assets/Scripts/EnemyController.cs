using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    [SerializeField, Header("最大HP")]
    private int maxHp;

    [SerializeField]
    private int hp;

    private Tween tween;

    private Vector3[] paths;

    private Animator anim;

    public int attackPoint;

    private GameManager gameManger;

    public EnemyDataSO.EnemyData enemyData;

    public void SetUpEnemyController(Vector3[] pathsData, GameManager gameManager, EnemyDataSO.EnemyData enemyData)
    {
        this.enemyData = enemyData;

        moveSpeed = this.enemyData.moveSpeed;

        attackPoint = this.enemyData.attackPower;

        maxHp = this.enemyData.hp;

        this.gameManger = gameManager;

        hp = maxHp;

        //Animatorコンポーネントを取得してanim変数に代入。GetComponentのように<型引数>を指定していないのは、anim変数によって型を推論することが可能であるため。
        TryGetComponent(out anim);

        paths = pathsData;

        //DOPathメソッドは配列の頂点を順に結ぶ
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);  //OnWaypointChangeメソッド内でChangeAnimeDirectionメソッドを実行する

        PauseMove();
    }

    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>
    private void ChangeAnimeDirection(int index)　　//OnWaypointChangeメソッドが取得したpathsの要素番号を受け取る
    {
        if (index >= paths.Length)
        {
            return;
        }

        //目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差を無くして一定値にする)
        Vector3 direction = (transform.position - paths[index]).normalized;

        //アニメーションのPalameterの値を更新し、移動アニメのBlendTreeを制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="amount"></param>
    public void CulcDamage(int amount)
    {
        //Hpの値を減算した結果値を、最低値と最大値の範囲内に収まるようにして更新
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);

        Debug.Log("残りHp : " + hp);

        if (hp <= 0)
        {
            DestroyEnemy();
        }

        //TODO 演出用のエフェクト生成

        //ヒットストップ演出
        StartCoroutine(WaitMove()); 
    }

    /// <summary>
    /// 敵破壊処理
    /// </summary>
    public void DestroyEnemy()
    {
        tween.Kill();

        // TODO SEの処理

        // TODO 破壊時のエフェクトの生成や関連する処理

        gameManger.CountUpDestroyEnemyCount(this);

        Destroy(gameObject);
    }

    /// <summary>
    /// 移動を一時停止
    /// </summary>
    public void PauseMove()
    {
        tween.Pause();
    }

    /// <summary>
    /// 移動を再開
    /// </summary>
    public void ResumeMove()
    {
        tween.Play();
    }

    /// <summary>
    /// ヒットストップ演出
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitMove()
    {
        tween.timeScale = 0.05f;

        yield return new WaitForSeconds(0.5f);

        tween.timeScale = 1.0f;
    }
}
