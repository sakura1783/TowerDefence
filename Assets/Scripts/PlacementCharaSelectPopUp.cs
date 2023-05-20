using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementCharaSelectPopUp : MonoBehaviour
{
    [SerializeField] private Button btnClosePopUp;

    [SerializeField] private Button btnChooseChara;

    [SerializeField] private CanvasGroup canvasGroup;

    private CharaGenerator charaGenerator;

    [SerializeField] private Image imgPickupChara;

    [SerializeField] private Text txtPickupCharaName;

    [SerializeField] private Text txtPickupCharaAttackPower;

    [SerializeField] private Text txtPickupCharaAttackRangeType;

    [SerializeField] private Text txtPickupCharaCost;

    [SerializeField] private Text txtPickupCharaMaxAttackCount;

    [SerializeField] private SelectCharaDetail selectCharaDetailPrefab;

    [SerializeField] private Transform selectCharaDetailTran;

    [SerializeField] private List<SelectCharaDetail> selectCharaDetailsList = new List<SelectCharaDetail>();

    private CharaData chooseCharaData;

    /// <summary>
    /// ポップアップの設定
    /// </summary>
    /// <param name="charaGenerator"></param>
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator, List<CharaData> haveCharaDataList)
    {
        this.charaGenerator = charaGenerator;

        //TODO 他に設定項目があったら追加する

        canvasGroup.alpha = 0;

        //各ボタンの操作を押せない状態にする
        SwitchActivateButtons(false);

        //スクリプタブル・オブジェクトに登録されているキャラ分(引数で受け取った情報)を利用して
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {
            //ボタンのゲームオブジェクトを生成
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);

            //ボタンのゲームオブジェクトの設定(CharaDataを設定する)
            selectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);

            //Listに追加
            selectCharaDetailsList.Add(selectCharaDetail);

            //最初に生成したボタンの場合
            if (i == 0)
            {
                //選択しているキャラとして初期値に設定
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }

        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);

        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        //ボタンを押せる状態にする
        SwitchActivateButtons(true);
    }

    /// <summary>
    /// 各ボタンのアクティブ状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateButtons(bool isSwitch)
    {
        btnChooseChara.interactable = isSwitch;
        btnClosePopUp.interactable = isSwitch;
    }

    /// <summary>
    /// ポップアップの表示
    /// </summary>
    public void ShowPopUp()
    {
        //各キャラのボタンの制御
        CheckAllCharaButtons();

        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// 選択しているキャラを配置するボタンを押した際の処理
    /// </summary>
    private void OnClickSubmitChooseChara()
    {
        //コストの支払いが可能か最終確認
        if (chooseCharaData.cost > GameData.instance.currency)
        {
            return;
        }

        //選択しているキャラの生成
        charaGenerator.CreateChooseChara(chooseCharaData);

        HidePopUp();
    }

    /// <summary>
    /// 配置をやめるボタンを押した際の処理
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

        canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivatePlacementCharaSelectPopUp());  //DOFadeを行った後に、InactivatePlacementCharaSelectPopUpメソッドが呼び出される。
    }

    public void SetSelectCharaDetail(CharaData charaData)
    {
        chooseCharaData = charaData;

        //各値の設定
        imgPickupChara.sprite = charaData.charaSprite;
        txtPickupCharaName.text = charaData.charaName;
        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();
        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();
        txtPickupCharaCost.text = charaData.cost.ToString();
        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();
    }

    /// <summary>
    /// コストが支払えるかどうかを各SelectCharaDetailで確認してボタン押下機能を切り替え
    /// </summary>
    private void CheckAllCharaButtons()
    {
        if (selectCharaDetailsList.Count > 0)
        {
            for (int i = 0; i < selectCharaDetailsList.Count; i++)
            {
                selectCharaDetailsList[i].ChangeActivateButton(selectCharaDetailsList[i].JudgePermissionCost(GameData.instance.currency));
            }
        }
    }
}
