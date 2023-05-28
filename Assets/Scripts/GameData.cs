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
    private const string CREAR_STAGE_KEY = "clearStagesList";

    /// <summary>
    /// セーブ・ロード用のクラス
    /// </summary>
    [System.Serializable]
    public class SaveData
    {
        public int clearPoint;
        public List<int> engageList = new List<int>();
        public List<int> clearedStageList = new List<int>();
    }

    private const string SAVE_KEY = "SaveData";  //SaveDataクラス用のKey

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

        //SaveClearStage();
        //LoadClearStage();


        //ユーザーデータの初期化
        Initialize();

        //ユーザーデータの初期化用のローカル関数
        void Initialize()
        {
            //SaveDataがセーブされているか確認
            if (PlayerPrefsHelper.ExistsData(SAVE_KEY))
            {
                //セーブされている場合のみロード
                GetSaveData();
            }
            else
            {
                //セーブされていなければ初期値設定
                totalClearPoint = 0;
            }

            if (!engageCharaNosList.Contains(0))
            {
                engageCharaNosList.Add(0);
            }

            if (!clearedStageNosList.Contains(0))
            {
                clearedStageNosList.Add(0);
            }
        }
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
        int result;

        if (int.TryParse(ENGAGE_CHARA_KEY, out result))
        {
            //成功した場合の処理
            Debug.Log(result);
        }
        else
        {
            Debug.Log("変換に失敗しました");
        }

        //この処理はLinqの機能を利用して記述されている
        //GameDataクラスのLoadEngageCharaListメソッド内の処理の、string型(1行のカンマ区切りの文字列)をList<int>型にする処理と同じ
        return str.Split(',').ToList().ConvertAll(x => int.Parse(x));  //ConvertAllで、コレクションの要素を一括して指定の型に変換することができる。ラムダ式内でint.Parseを使用することで、要素をint型に変換している
    }

    /// <summary>
    /// クリアしたステージをセーブ
    /// </summary>
    public void SaveClearStage()
    {
        //新しく作成する文字列
        //string clearStageString = "";

        //契約したキャラのListをカンマ区切りの1行の文字列にする
        //for (int i = 0; i < clearedStageNosList.Count; i++)
        //{
        //clearStageString += clearedStageNosList[i].ToString();
        //}

        //文字列をセットしてセーブ
        //PlayerPrefs.SetString(CREAR_STAGE_KEY, clearStageString);

        //上記の処理を1行で記述する。
        PlayerPrefs.SetString(CREAR_STAGE_KEY, StageConvertListToString(clearedStageNosList));
        PlayerPrefs.Save();
    }

    public string StageConvertListToString(List<int> listData)
    {
        return listData.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b);
    }

    /// <summary>
    /// クリアしたステージをロード
    /// </summary>
    public void LoadClearStage()
    {
        //文字列としてロード
        //string clearStageString = PlayerPrefs.GetString(CREAR_STAGE_KEY, "");

        //ロードした文字列がある場合
        //if (!string.IsNullOrEmpty(clearStageString))
        //{
        //カンマの位置で区切って、文字列の配列を作成。その後、最後にできる空白の文字列を削除
        //string[] strArray = clearStageString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //Debug.Log(strArray.Length);

        //配列の数だけクリアしたステージの情報があるので
        //for (int i = 0; i < strArray.Length; i++)
        //{
        //Debug.Log(strArray[i]);

        //配列の文字列の値をint型に変換してListに追加して、クリアしたステージのListを復元
        //clearedStageNosList.Add(int.Parse(strArray[i]));
        //}
        //}

        //上記の処理を1行で記述する
        clearedStageNosList = StageConvertStringToList(PlayerPrefs.GetString(CREAR_STAGE_KEY, ""));
    }

    public List<int> StageConvertStringToList(string str)
    {
        return str.Split(',').ToList().ConvertAll(x => int.Parse(x));
    }


    /// <summary>
    /// セーブする値をSaveDataに設定してセーブ
    /// セーブするタイミングは、ステージクリア時、キャラ契約時
    /// </summary>
    public void SetSaveData()
    {
        //セーブ用のデータを作成
        SaveData saveData = new SaveData
        {
            //各値をSaveDataクラスの変数に設定
            clearPoint = totalClearPoint,
            engageList = engageCharaNosList,
            clearedStageList = clearedStageNosList
        };

        //SaveDataクラスとしてSAVE_KEYの名前でセーブ
        PlayerPrefsHelper.SaveSetObjectData(SAVE_KEY, saveData);

        Debug.Log("セーブしました");
    }

    /// <summary>
    /// SaveDataをロードして、各値に設定
    /// </summary>
    public void GetSaveData()
    {
        //SaveDataとしてロード
        SaveData saveData = PlayerPrefsHelper.LoadGetObjectData<SaveData>(SAVE_KEY);

        //各値にSaveData内の値を設定
        totalClearPoint = saveData.clearPoint;
        engageCharaNosList = saveData.engageList;
        clearedStageNosList = saveData.clearedStageList;

        Debug.Log("ロードしました");
    }
}
