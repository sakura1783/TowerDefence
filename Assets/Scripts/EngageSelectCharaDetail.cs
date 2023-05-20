using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EngageSelectCharaDetail : MonoBehaviour
{
    [SerializeField] private Button btnSelectCharaDetail;

    [SerializeField] private Image imgChara;

    private PlacementEngageCharaPopUp placementEngageCharaPopUp;

    private CharaData charaData;

    /// <summary>
    /// EngageSelectCharaDetailの設定
    /// </summary>
    /// <param name="placementEngageCharaPop"></param>
    /// <param name="charaData"></param>
    public void SetUpEngageSelectCharaDetail(PlacementEngageCharaPopUp placementEngageCharaPop, CharaData charaData)
    {
        this.placementEngageCharaPopUp = placementEngageCharaPop;
        this.charaData = charaData;

        ChangeActivateButton(false);

        imgChara.sprite = this.charaData.charaSprite;

        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);

        //クリアポイントに応じてボタンを押せるかどうかを切り替える
        ChangeActivateButton(JudgePermissionEngageCost(GameData.instance.totalClearPoint));
    }

    /// <summary>
    /// EngageSelectCharaDetailを押した時の処理
    /// </summary>
    private void OnClickSelectCharaDetail()
    {
        //TODO アニメ演出

        //タップしたEngageSelectCharaDetailの情報をポップアップに送る
        placementEngageCharaPopUp.SetSelectCharaDetail(charaData);
    }

    /// <summary>
    /// ボタンを押せる状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void ChangeActivateButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    public bool JudgePermissionEngageCost(int value)
    {
        //契約料が支払える場合
        if (charaData.engageCost <= value)
        {
            ChangeActivateButton(true);
            return true;
        }
        return false;
    }
}
