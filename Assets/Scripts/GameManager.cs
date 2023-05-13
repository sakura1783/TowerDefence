using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField] private CharaGenerator charaGenerator;

    public bool isEnemyGenerate;

    public int generateIntervalTime;

    public int generateEnemyCount;

    public int maxEnemyCount;

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {
        Prepare,  //ゲーム開始前の準備中
        Play,  //ゲームプレイ中
        Stop,  //ゲーム内の処理の一時停止中
        GameUp  //ゲーム終了(ゲームオーバー、ゲームクリア)
    }

    public GameState currentGameState;

    [SerializeField] private List<EnemyController> enemiesList = new List<EnemyController>();

    private int destroyEnemyCount;

    public UIManager uiManager;

    [SerializeField] private List<CharaController> charasList = new List<CharaController>();

    [SerializeField] private DefenceBase defenceBase;

    [SerializeField] private MapInfo currentMapInfo;

    [SerializeField] private DefenceBase defenceBasePrefab;

    [SerializeField] private StageData currentStageData;

    void Start()
    {
        SetGameState(GameState.Prepare);

        //TODO ゲームデータを初期化

        //ステージの設定 + ステージごとのPathDataを設定
        SetUpStageData();

        //キャラ配置用ポップアップの生成と設定
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        //拠点の設定
        defenceBase.SetUpDefenceBase(this, currentStageData.defenceBaseLife, uiManager);

        //TODO オープニング演出設定

        isEnemyGenerate = true;

        SetGameState(GameState.Play);

        StartCoroutine(enemyGenerator.PrepareEnemyGenerate(this, currentStageData));  //thisでGameManagerクラスを渡している

        //カレンシーの自動獲得処理の開始
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// 敵の情報をListに追加
    /// </summary>
    public void AddEnemyList(EnemyController enemy)   //敵の情報をListに登録する際に、引数を追加
    {
        //敵の情報をListに追加
        enemiesList.Add(enemy);

        generateEnemyCount++;
    }

    /// <summary>
    /// 敵の生成を停止するか判定
    /// </summary>
    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }

    /// <summary>
    /// GameStateの変更
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    /// <summary>
    /// 全ての敵の移動を一時停止
    /// </summary>
    public void PauseEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    /// <summary>
    /// 全ての敵の移動を再開
    /// </summary>
    public void ResumeEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    /// <summary>
    /// 敵の情報をListから削除
    /// </summary>
    /// <param name="removeEnemy"></param>
    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    /// <summary>
    /// 破壊した敵の数をカウント(このメソッドを外部のクラスから実行してもらう)
    /// </summary>
    /// <param name="enemyController"></param>
    public void CountUpDestroyEnemyCount(EnemyController enemyController)
    {
        RemoveEnemyList(enemyController);

        destroyEnemyCount++;

        Debug.Log("破壊した敵の数" + destroyEnemyCount);

        JudgeGameClear();
    }

    /// <summary>
    /// ゲームクリア判定
    /// </summary>
    public void JudgeGameClear()
    {
        if (destroyEnemyCount >= maxEnemyCount)
        {
            Debug.Log("ゲームクリア");

            //TODO ゲームクリアの処理を追加
        }
    }

    /// <summary>
    /// 時間の経過に応じてカレンシーを加算
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimeToCurrency()
    {
        int timer = 0;

        while (currentGameState == GameState.Play)
        {
            timer++;

            if (timer > GameData.instance.currencyIntervalTime && GameData.instance.currency < GameData.instance.maxCurrency)
            {
                timer = 0;

                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);

                uiManager.UpdateDisplayCurrency();
            }

            yield return null;
        }
    }

    /// <summary>
    /// 選択したキャラの情報をListに追加
    /// </summary>
    /// <param name="chara"></param>
    public void AddCharasList(CharaController chara)
    {
        charasList.Add(chara);
    }

    /// <summary>
    /// 選択したキャラを破棄し、情報をListから削除
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharasList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charasList.Remove(chara);
    }

    /// <summary>
    /// 現在の配置しているキャラの数の取得
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charasList.Count;
    }

    /// <summary>
    /// 配置解除を選択するポップアップ作成の準備
    /// </summary>
    /// <param name="chara"></param>
    public void PrepareCreateReturnCharaPopUp(CharaController chara)
    {
        SetGameState(GameState.Stop);

        //全ての敵の移動を一時停止
        PauseEnemies();

        //配置解除を選択するポップアップを作成
        uiManager.CreateReturnCharaPopUp(chara, this);
    }

    /// <summary>
    /// 選択したキャラの配置解除の確認(ReturnSelectCharaPopUpから呼び出される)
    /// </summary>
    /// <param name="isReturnChara"></param>
    /// <param name="chara"></param>
    public void JudgeReturnChara(bool isReturnChara, CharaController chara)
    {
        if (isReturnChara)
        {
            RemoveCharasList(chara);
        }

        SetGameState(GameState.Play);

        //全ての敵の移動を再開
        ResumeEnemies();

        //カレンシーの加算処理を再開
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// ステージデータの設定
    /// </summary>
    private void SetUpStageData()
    {
        //GameDataのstageNoからStageDataを取得
        currentStageData = DataBaseManager.instance.stageDataSO.stageDatasList[GameData.instance.stageNo];

        //各情報をStageDataクラスを参照して設定
        generateIntervalTime = currentStageData.generateIntervalTime;
        maxEnemyCount = currentStageData.mapInfo.appearEnemyInfos.Length;

        //ステージ用のマップと防衛拠点の生成
        currentMapInfo = Instantiate(currentStageData.mapInfo);
        defenceBase = Instantiate(defenceBasePrefab, currentMapInfo.GetDefenceBaseTran());

        //PathDatasの移動経路情報をStageDataクラスを参照して設定
        PathData[] pathDatas = new PathData[currentStageData.mapInfo.appearEnemyInfos.Length];
        for (int i = 0; i < currentStageData.mapInfo.appearEnemyInfos.Length; i++)
        {
            pathDatas[i] = currentStageData.mapInfo.appearEnemyInfos[i].enemyPathData;
        }

        //移動経路の情報を引数で渡して、EnemyGeneratorクラスの設定を行う
        enemyGenerator.SetUpPathDatas(pathDatas);

        //TODO 他にもあれば追加
    }
}
