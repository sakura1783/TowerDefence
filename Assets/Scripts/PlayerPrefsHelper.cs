using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerPrefsHelper
{
    /// <summary>
    /// 指定したキーのデータが存在しているか確認
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool ExistsData(string key)
    {
        //指定したキーのデータが存在しているか確認して、存在している場合はtrue、存在していない場合はfalseを戻す
        return PlayerPrefs.HasKey(key);
    }

    /// <summary>
    /// 指定されたオブジェクトのデータをセーブ
    /// </summary>
    /// <typeparam name="T">セーブする型</typeparam>
    /// <param name="key">データを識別するためのキー/param>
    /// <param name="obj">セーブする情報</param>
    public static void SaveSetObjectData<T>(string key, T obj)
    {
        //オブジェクトのデータをJson形式に変換
        string json = JsonUtility.ToJson(obj);

        //セット
        PlayerPrefs.SetString(key, json);

        //セットしたKeyとjsonをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 指定されたオブジェクトのデータをロード
    /// </summary>
    /// <typeparam name="T">ロードする型</typeparam>
    /// <param name="key">データを識別するためのキー</param>
    /// <returns></returns>
    public static T LoadGetObjectData<T>(string key)
    {
        //セーブされているデータをロード
        string json = PlayerPrefs.GetString(key);

        //読み込む型を指定して変換して取得
        return JsonUtility.FromJson<T>(json);  //この場合はjsonをT型に変換
    }

    /// <summary>
    /// 指定されたキーのデータを削除
    /// </summary>
    /// <param name="key"></param>
    public static void RemoveObjectData(string key)
    {
        //指定されたキーのデータを削除
        PlayerPrefs.DeleteKey(key);

        Debug.Log("セーブデータを削除　実行 : " + key);
    }

    /// <summary>
    /// 全てのセーブデータを削除
    /// </summary>
    public static void AllClearSaveData()
    {
        //全てのセーブデータを削除
        PlayerPrefs.DeleteAll();

        Debug.Log("全セーブデータを削除　実行");
    }
}
