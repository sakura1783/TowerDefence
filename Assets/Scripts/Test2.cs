using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test2 : MonoBehaviour
{
    private int totalClearPoint;

    [SerializeField] private int clearPoint;

    [SerializeField] private Test1 test1;

    void Start()
    {
        totalClearPoint = totalClearPoint + clearPoint;

        test1.DisplayTotal();
    }
}
