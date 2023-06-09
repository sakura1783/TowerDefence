using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtons : MonoBehaviour
{
    [SerializeField] private Button stageButton;  //最終的にStageButtonsの変数はstageButtonだけにする？

    //[SerializeField] private PlacementSelectStagePopUp placementSelectStagePopUp;

    //[SerializeField] private StageData stageData;  //このままだとアサインできないので違う方法を考える。メソッドの引数として受け取る、渡す

    private StageData stageData;

    private PlacementSelectStagePopUp placementSelectStagePopUp;

    public int stageNo;

    /// <summary>
    /// ステージ選択ボタンの設定
    /// </summary>
    /// <param name="stageData"></param>
    /// <param name="placementSelectStagePopUp"></param>
    public void SetUpStageButtons(StageData stageData, PlacementSelectStagePopUp placementSelectStagePopUp)
    {
        this.stageData = stageData;
        this.placementSelectStagePopUp = placementSelectStagePopUp;

        stageButton.onClick.AddListener(() => OnClickStageButton(stageData, placementSelectStagePopUp));
        //stageButton.onClick.AddListener(() => OnClickStageButton());

        //各ステージの情報を設定する
        SetSelectStageInfo();
    }

    /// <summary>
    /// ステージ選択のボタンを押した際の処理
    /// </summary>
    private void OnClickStageButton(StageData stageData, PlacementSelectStagePopUp placementSelectStagePopUp)
    {
        //TODO ボタンを押したらポップアップを開く。StageDataも渡す
        placementSelectStagePopUp.ShowPopUp(stageData);

        Debug.Log("OnClickStageButton");
    }

    public void OnClickStageButton()
    {
        placementSelectStagePopUp.SetUpPlacementSelectStagePopUp();
    }

    /// <summary>
    /// ステージ情報の設定
    /// </summary>
    private void SetSelectStageInfo()
    {

    }
}
