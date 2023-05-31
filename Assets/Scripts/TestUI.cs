using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    private int totalClearPoint;

    [SerializeField] private int clearPoint;

    [SerializeField] private Text txtTotalClearPoint;

    void Start()
    {
        totalClearPoint = totalClearPoint + clearPoint;

        DisplayTotal();
    }

    private void DisplayTotal()
    {
        txtTotalClearPoint.text = totalClearPoint.ToString();
    }
}
