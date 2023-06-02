using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI表示をするクラス、Worldシーンに用意する。WorldUIManagerに記述する。EngageManagerのStartメソッドに呼び出し命令を記述する。  //完了
/// </summary>
public class Test1 : MonoBehaviour
{
    [SerializeField] Text txtTotalClearPoint;

    public void DisplayTotal(int totalClearPoint)
    {
        //txtTotalClearPoint.text = GameData.instance.totalClearPoint.ToString();

        //Test4のAddClearPointメソッドで加算したtotalClearPoint変数を受け取って表示させる
        txtTotalClearPoint.text = totalClearPoint.ToString();
    }
}
