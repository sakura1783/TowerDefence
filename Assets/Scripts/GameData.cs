using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    private const string ENGAGE_CHARA_KEY = "engageCharaNosList";  //契約したキャラのセーブ用Key

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

        //SaveEngageCharaList();
        //LoadEngageCharaList();
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

    /// <summary>
    /// engageCharaNosListの値をセーブ
    /// </summary>
    public void SaveEngageCharaList()
    {
        //新しく作成する文字列
        //string engageCharaListString = "";

        //契約したキャラのListをカンマ区切りの1行の文字列にする
        //for (int i = 0; i < engageCharaNosList.Count; i++)
        //{
        //engageCharaListString += engageCharaNosList[i].ToString();
        //}

        //文字列をセットしてセーブ
        //PlayerPrefs.SetString(ENGAGE_CHARA_KEY, engageCharaListString);

        //上記の処理を1行で記述する。SetStringメソッドの第二引数内ではConvertListToStringメソッドを実行し、string型の戻り値を受け取り、それを第二引数として設定している
        PlayerPrefs.SetString(ENGAGE_CHARA_KEY, ConvertListToString(engageCharaNosList));
        PlayerPrefs.Save();

        Debug.Log("EngageCharaListをセーブしました");
    }

    /// <summary>
    /// int型のListの値をカンマ区切りの1行のstring型に変換
    /// </summary>
    /// <param name="listData"></param>
    /// <returns></returns>
    public string ConvertListToString(List<int> listData)
    {
        //この処理はLinqの機能を利用して記述されている
        //この処理は、GameDataクラスのSaveEngageCharaDataメソッド内の処理の、List<int>型の情報をstring型(1行のカンマ区切りの文字列)にする処理と同じもの。
        return listData.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b);  //listData内の情報をToStringで文字列にし、それをAggregateでカンマで連結する
    }

    /// <summary>
    /// engageCharaNosListの値をロード
    /// </summary>
    public void LoadEngageCharaList()
    {
        //文字列としてロード
        //string engageCharaListString = PlayerPrefs.GetString(ENGAGE_CHARA_KEY, "");

        //ロードした文字列がある場合
        //if (!string.IsNullOrEmpty(engageCharaListString))
        //{
        //カンマの位置で区切って、文字列の配列を作成。その際、最後にできる空白の文字列を削除
        //string[] strArray = engageCharaListString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //Debug.Log(strArray.Length);

        //配列の数だけ契約したキャラの情報があるので
        //for (int i = 0; i < strArray.Length; i++)
        //{
        //Debug.Log(strArray[i]);

        //配列の文字列の値をint型に変換してListに追加して、契約キャラのListを復元
        //engageCharaNosList.Add(int.Parse(strArray[i]));
        //}
        //}

        //上記の処理を1行で記述する。ConvertStringToListメソッドの引数としてGetStringメソッドを実行し、List<int>型の戻り値を受け取り、それをengageCharaNosListに設定している
        engageCharaNosList = ConvertStringToList(PlayerPrefs.GetString(ENGAGE_CHARA_KEY, ""));

        Debug.Log("EngageCharaListをロードしました");
    }

    /// <summary>
    /// カンマ区切りになっている1行のstring型の値をint型のListに変換
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public List<int> ConvertStringToList(string str)
    {
        //この処理はLinqの機能を利用して記述されている
        //GameDataクラスのLoadEngageCharaListメソッド内の処理の、string型(1行のカンマ区切りの文字列)をList<int>型にする処理と同じ
        return str.Split(',').ToList().ConvertAll(x => int.Parse(x));  //ConvertAllで、コレクションの要素を一括して指定の型に変換することができる。ラムダ式内でint.Parseを使用することで、要素をint型に変換している
    }
}
