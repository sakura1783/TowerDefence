using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WorldUIManager : MonoBehaviour
{
    [SerializeField] private Text txtTotalClearPoint;

    public void UpdateTotalClearPoint()
    {
        txtTotalClearPoint.text = GameData.instance.totalClearPoint.ToString();
    }
}
