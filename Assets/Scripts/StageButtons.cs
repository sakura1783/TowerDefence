using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtons : MonoBehaviour
{
    [SerializeField] private Button stageButton;

    [SerializeField] private PlacementSelectStagePopUp placementSelectStagePopUp;

    [SerializeField] private StageData stageData;

    public void SetUpStageButtons()
    {
        stageButton.onClick.AddListener(OnClickStageButton);
    }

    /// <summary>
    /// ステージ選択のボタンを押した際の処理
    /// </summary>
    private void OnClickStageButton()
    {
        placementSelectStagePopUp.SetUpPlacementSelectStagePopUp(stageData);
    }
}
