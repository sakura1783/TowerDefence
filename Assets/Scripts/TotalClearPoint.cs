using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalClearPoint : MonoBehaviour
{
    [SerializeField] private Text txtTotalClearPoint;

    private StageData stageData;

    private GameData gameData;

    private void SetUpTotalClearPoint(StageData stageData, GameData gameData)
    {
        this.stageData = stageData;
        this.gameData = gameData;

        txtTotalClearPoint.text = gameData.totalClearPoint.ToString();
    }

    public void UpdateTotalClearPoint()
    {
        txtTotalClearPoint.text += stageData.clearPoint;
    }
}
