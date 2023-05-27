using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementEngageCharaPopUp : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnEngageChara;

    [SerializeField] private CanvasGroup placementEngageCharaCanvasGroup;
    [SerializeField] private CanvasGroup sumi_CanvasGroup;
    [SerializeField] private CanvasGroup key_CanvasGroup;

    private EngageManager engageManager;

    [SerializeField] private Image imgSelectChara;

    [SerializeField] private Text txtCharaName;
    [SerializeField] private Text txtAttackPoint;
    [SerializeField] private Text txtAttackRange;
    [SerializeField] private Text txtCost;
    [SerializeField] private Text txtMaxAttackCount;
    [SerializeField] private Text txtCharaSummary;
    [SerializeField] private Text txtEngageCost;

    [SerializeField] private EngageSelectCharaDetail engageSelectCharaDetailPrefab;

    [SerializeField] private Transform engageSelectCharaDetailTran;

    [SerializeField] private List<EngageSelectCharaDetail> engageSelectCharaDetailsList = new List<EngageSelectCharaDetail>();  //生成したキャラのボタンを管理する

    private CharaData chooseCharaData;  //現在選択しているキャラの情報を管理する

    /// <summary>
    /// ポップアップの設定
    /// </summary>
    /// <param name="charaGenerator"></param>
    /// <param name="haveCharaDatasList"></param>
    public void SetUpPlacementEngageCharaPopUp(EngageManager engageManager, List<CharaData> haveCharaDatasList)
    {
        this.engageManager = engageManager;

        //TODO 他に設定項目があったら追加する

        placementEngageCharaCanvasGroup.alpha = 0;

        SwitchActivateButtons(false);

        Debug.Log("Listの数" + haveCharaDatasList.Count);
        //スクリプタブル・オブジェクトに登録されているキャラ分
        for (int i = 0; i < haveCharaDatasList.Count; i++)
        {
            //ボタンのゲームオブジェクトを作成
            EngageSelectCharaDetail engageSelectCharaDetail = Instantiate(engageSelectCharaDetailPrefab, engageSelectCharaDetailTran, false);

            //ボタンのゲームオブジェクトの設定(CharaDataを設定する)
            engageSelectCharaDetail.SetUpEngageSelectCharaDetail(this, haveCharaDatasList[i]);

            //Listに追加
            engageSelectCharaDetailsList.Add(engageSelectCharaDetail);

            //最初に生成したボタンの場合
            if (i == 0)
            {
                //選択しているキャラとして初期値に設定
                SetSelectCharaDetail(haveCharaDatasList[i]);
            }
        }

        btnEngageChara.onClick.AddListener(OnClickEngageChara);
        btnClose.onClick.AddListener(OnClickClosePopUp);

        SwitchActivateButtons(true);
    }

    /// <summary>
    /// 各ボタンのアクティブ状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateButtons(bool isSwitch)
    {
        btnEngageChara.interactable = isSwitch;
        btnClose.interactable = isSwitch;
    }

    /// <summary>
    /// ポップアップの表示
    /// </summary>
    public void ShowPopUp()
    {
        //各キャラのボタンの制御
        CheckAllCharaButtons();

        placementEngageCharaCanvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// 選択しているキャラを契約するボタンを押した際の処理
    /// </summary>
    private void OnClickEngageChara()
    {
        //契約料の支払いが可能か最終確認
        if (chooseCharaData.engageCost > GameData.instance.totalClearPoint)
        {
            return;
        }

        //TODO 選択しているキャラとの契約

        HidePopUp();
    }

    /// <summary>
    /// 契約をやめるボタンを押した際の処理
    /// </summary>
    private void OnClickClosePopUp()
    {
        HidePopUp();
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    private void HidePopUp()
    {
        //各キャラのボタンの制御
        CheckAllCharaButtons();

        placementEngageCharaCanvasGroup.DOFade(0, 0.5f).OnComplete(() => engageManager.InactivatePlacementEngageCharaPopUp());
    }

    /// <summary>
    /// 選択されたEngageSelectCharaDetailの情報をポップアップ内のピックアップに表示する
    /// </summary>
    /// <param name="charaData"></param>
    public void SetSelectCharaDetail(CharaData charaData)
    {
        chooseCharaData = charaData;

        //各値の設定
        imgSelectChara.sprite = charaData.charaSprite;
        txtCharaName.text = charaData.charaName;
        txtAttackPoint.text = charaData.attackPower.ToString();
        txtAttackRange.text = charaData.attackRange.ToString();
        txtCost.text = charaData.cost.ToString();
        txtMaxAttackCount.text = charaData.maxAttackCount.ToString();
        txtCharaSummary.text = charaData.discription;
        txtEngageCost.text = charaData.engageCost.ToString();
    }

    /// <summary>
    /// 契約料を支払えるかどうかを各EngageSelectCharaDetailで確認してボタン押下機能を切り替え
    /// </summary>
    private void CheckAllCharaButtons()
    {
        if (engageSelectCharaDetailsList.Count > 0)
        {
            for (int i = 0; i < engageSelectCharaDetailsList.Count; i++)
            {
                engageSelectCharaDetailsList[i].ChangeActivateButton(engageSelectCharaDetailsList[i].JudgePermissionEngageCost(GameData.instance.totalClearPoint));
            }
        }
    }
}
