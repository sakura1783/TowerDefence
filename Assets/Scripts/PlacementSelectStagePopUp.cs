using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementSelectStagePopUp : MonoBehaviour
{
    [SerializeField] private Button btnCancel;
    [SerializeField] private Button btnEnter;

    [SerializeField] private Text txtStageName;
    [SerializeField] private Text txtClearPoint;
    [SerializeField] private Text txtDefenceBaseLife;
    [SerializeField] private Text txtMaxCharaCount;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private Transform canvasTran;

    private StageData chooseStageData;

    [SerializeField] private EngageManager engageManager;

    /// <summary>
    /// ポップアップの設定
    /// </summary>
    public void SetUpPlacementSelectStagePopUp(StageData stageData)
    {
        //TODO 設定項目があれば追加する

        canvasGroup.alpha = 0;

        SwitchActivateButtons(false);

        btnCancel.onClick.AddListener(OnClickCancel);
        btnEnter.onClick.AddListener(OnClickEnter);

        SetSelectStageDetail(stageData);

        SwitchActivateButtons(true);
    }

    /// <summary>
    /// ボタンのアクティブ状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    private void SwitchActivateButtons(bool isSwitch)
    {
        btnCancel.interactable = isSwitch;
        btnEnter.interactable = isSwitch;
    }

    /// <summary>
    /// ポップアップの表示
    /// </summary>
    public void ShowPopUp()
    {
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// 止めるボタンを押した際の処理
    /// </summary>
    private void OnClickCancel()
    {
        SwitchActivateButtons(false);

        HidePopUp();
    }

    private void OnClickEnter()
    {
        HidePopUp();

        //TODO 破棄されないゲームオブジェクトにアタッチされているクラスに、ステージの情報を保存する
        //GameData.instance.stageNo = 

        SceneStageManager.instance.PrepareNextScene(SceneType.Battle);
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => engageManager.InactivatePlacementSelectStagePopUp());
    }

    public void SetSelectStageDetail(StageData stageData)
    {
        chooseStageData = stageData;

        //各値の設定
        txtStageName.text = stageData.stageName;
        txtClearPoint.text = stageData.clearPoint.ToString();
        txtDefenceBaseLife.text = stageData.defenceBaseLife.ToString();
        txtMaxCharaCount.text = stageData.maxCharaPlacementCount.ToString();
    }
}
