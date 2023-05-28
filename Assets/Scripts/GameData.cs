using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("コスト用の通貨")] public int currency;

    [Header("カレンシーの最大値")] public int maxCurrency;

    [Header("加算までの待機時間")] public int currencyIntervalTime;

    [Header("加算値")] public int addCurrencyPoint;

    public int macCharaPlacementCount;  //配置できるキャラの上限数

    [Header("デバックモードの切り替え")] public bool isDebug;

    public int defenceBaseLife;

    public int stageNo;

    public int totalClearPoint;

    [Header("契約して所持しているキャラの番号")] public List<int> engageCharaNosList = new List<int>();

    [Header("表示するステージの番号")] public List<int> clearedStageNosList = new List<int>();

    private const string CREAR_POINT_KEY = "clearPoint";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //セーブ・ロードのDebug用
        //SaveClearPoint();
        //LoadClearPoint();
    }

    /// <summary>
    /// TotalClearPointの値をセーブ
    /// </summary>
    public void SaveClearPoint()
    {
        //セーブするための準備・セット
        PlayerPrefs.SetInt(CREAR_POINT_KEY, totalClearPoint);

        //セーブ実行
        PlayerPrefs.Save();

        Debug.Log("セーブ : " + CREAR_POINT_KEY + " : " + totalClearPoint);
    }

    public void LoadClearPoint()
    {
        //Keyを使用してロードを行い、戻り値を代入
        totalClearPoint = PlayerPrefs.GetInt(CREAR_POINT_KEY, 0);  //第二引数はキーに対応するセーブデータがなかった時に返す値

        Debug.Log("ロード : " + CREAR_POINT_KEY + " : " + totalClearPoint);
    }
}
