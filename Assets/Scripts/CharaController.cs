using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    private int attackPower = 1;

    [SerializeField, Header("攻撃までの待機時間")]
    private float intervalAttackTime = 120.0f;

    [SerializeField]
    private bool isAttack;

    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private int attackCount = 3;  //TODO 現在の攻撃回数の残り。あとでCharaDataクラスの値を反映させる

    [SerializeField]
    private Text txtAttackCount;

    [SerializeField] private BoxCollider2D attackRangeArea;

    [SerializeField] private CharaData charaData;

    private GameManager gameManger;

    //private SpriteRenderer spriteRenderer;

    private Animator anim;

    private string overrideClipName = "Chara_3_Base";

    private AnimatorOverrideController overrideController;  //AnimatorOverrideControllerは、特定のアニメーションを置き換えるためのアセット

    void Start()
    {
        Application.targetFrameRate = 60;

        //自分で追加したよ
        txtAttackCount.text = attackCount.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttack && !enemy)
        {
            Debug.Log("敵発見");

            //敵の情報(EnemyController)を取得する。EnemyControllerがアタッチされているゲームオブジェクトを判別しているので、ここで、今までのTagによる判定と同じ動作で判定が行われる。
            if (collision.gameObject.TryGetComponent(out enemy))
            {
                isAttack = true;

                StartCoroutine(PrepareAttack());

                Debug.Log("コルーチンスタート");
            }
        }
    }

    /// <summary>
    /// 攻撃準備
    /// </summary>
    private IEnumerator PrepareAttack()
    {
        Debug.Log("攻撃準備開始" + isAttack);

        int timer = 0;

        while (isAttack)
        {
            //ゲームプレイ中のみ攻撃する
            if (gameManger.currentGameState == GameManager.GameState.Play)
            {

                timer++;

                if (timer > intervalAttackTime)
                {
                    timer = 0;

                    Attack();

                    //攻撃回数関連の処理をここに記述する
                    attackCount--;

                    //残り攻撃回数の表示更新
                    UpdateDisplayAttackCount();

                    if (attackCount <= 0)
                    {
                        Destroy(gameObject);
                        gameManger.RemoveCharasList(this);
                    }
                }
            }

            //1フレーム処理を中断する(これを書き忘れると無限ループになり、Unityエディターが動かなくなって再起動することになる。注意！)
            yield return null;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        Debug.Log("攻撃");

        // TODO キャラの上に攻撃エフェクトを追加

        enemy.CulcDamage(attackPower);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("敵なし");

        isAttack = false;
        enemy = null;
    }

    private void UpdateDisplayAttackCount()
    {
        txtAttackCount.text = attackCount.ToString();
    }

    /// <summary>
    /// キャラの設定
    /// </summary>
    /// <param name="charaData"></param>
    /// <param name="gameManger"></param>
    public void SetUpChara(CharaData charaData, GameManager gameManger)
    {
        this.charaData = charaData;
        this.gameManger = gameManger;

        //各値をCharaDataから取得して設定
        attackPower = this.charaData.attackPower;
        intervalAttackTime = this.charaData.intervalAttackTime;

        //DataBaseMangerに登録されているAttackRangeSizeSOスクリプタブル・オブジェクトのデータと照合を行い、CharaDataのAttackRangeTypeの情報をもとにSizeを決定
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);

        attackCount = this.charaData.maxAttackCount;

        //残りの攻撃回数の表示更新
        UpdateDisplayAttackCount();

        //キャラ画像の設定。アニメを利用するようになったら、この処理はやらない
        //if (TryGetComponent(out spriteRenderer))
        //{
        //spriteRenderer.sprite = this.charaData.charaSprite;
        //}

        //キャラごとのAnimationClipを設定
        SetUpAnimation();
    }

    private void SetUpAnimation()
    {
        if (TryGetComponent(out anim))
        {
            overrideController = new AnimatorOverrideController();

            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;  //runtimeAnimatorControllerで実行中にAnimatorControllerを変更する
            anim.runtimeAnimatorController = overrideController;

            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];

            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);  //GetCurrentAnimatorStateInfoでAnimatorの現在の状態からデータを取得する。例えば、速度、長さ、名前、その他の変数など
            }

            overrideController[overrideClipName] = this.charaData.charaAnim;

            anim.runtimeAnimatorController = overrideController;  

            anim.Update(0.0f);  

            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
            }
        }
    }

    /// <summary>
    /// キャラをタップした際の処理(EventTriggerに登録するメソッド)
    /// </summary>
    public void OnClickChara()
    {
        //GameManagerを経由して、ゲームの進行状態を切り替えつつ、UIManagerへ処理を繋げてもらうためのメソッド
        gameManger.PrepareCreateReturnCharaPopUp(this);
    }
}
