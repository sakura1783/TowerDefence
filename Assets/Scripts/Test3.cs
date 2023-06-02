using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加算ポイントを管理するクラス。GameManagerに記述する。  //完了
/// </summary>
public class Test3 : MonoBehaviour
{
    [SerializeField] private int clearPoint;

    [SerializeField] private Test4 test4;
    [SerializeField] private Test1 test1;

    void Start()
    {
        test4.AddClearPoint(clearPoint);

        test1.DisplayTotal(test4.totalClearPoint);
    }
}
