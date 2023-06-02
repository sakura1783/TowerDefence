using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トータルの値を管理するクラス。GameDataがこの役割を持つ。  //完了
/// </summary>
public class Test4 : MonoBehaviour
{
    public int totalClearPoint;

    /// <summary>
    /// クリアポイント加算用のメソッド
    /// </summary>
    /// <param name="clearPoint"></param>
    public void AddClearPoint(int clearPoint)
    {
        totalClearPoint = totalClearPoint + clearPoint;
    }
}
