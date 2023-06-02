using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtons : MonoBehaviour
{
    [SerializeField] private Button stageButton;

    [SerializeField] private Button btnEnter;

    [SerializeField] private PlacementSelectStagePopUp placementSelectStagePopUp;

    [SerializeField] private StageData stageData;

    public void SetUpStageButtons()
    {
        stageButton.onClick.AddListener(OnClickStageButton);
        btnEnter.onClick.AddListener(OnClickEnter);
    }

    /// <summary>
    /// ステージ選択のボタンを押した際の処理
    /// </summary>
    private void OnClickStageButton()
    {
        placementSelectStagePopUp.SetUpPlacementSelectStagePopUp(stageData);
    }

    /// <summary>
    /// 「ゲームを開始する」ボタンを押した際の処理
    /// </summary>
    private void OnClickEnter()
    {
        //TODO それぞれのステージに飛ぶ
    }
}
