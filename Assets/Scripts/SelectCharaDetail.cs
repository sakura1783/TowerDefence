using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour
{
    [SerializeField] private Button btnSelectCharaDetail;

    [SerializeField] private Image imgChara;

    private PlacementCharaSelectPopUp placementCharaSelectPop;

    private CharaData charaData;

    /// <summary>
    /// SelectCharaDetailの設定
    /// </summary>
    /// <param name="placementCharaSelectPop"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPop, CharaData charaData)
    {
        this.placementCharaSelectPop = placementCharaSelectPop;
        this.charaData = charaData;

        //ボタンを押せない状態に切り替える
        ChangeActivateButton(false);

        imgChara.sprite = this.charaData.charaSprite;  //thisを書かないと、引数で受け取った側のcharaDataになってしまう。

        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);

        //コストに応じてボタンを押せるかどうかを切り替える
        ChangeActivateButton(JudgePermissionCost(GameData.instance.currency));
    }

    /// <summary>
    /// SelectCharaDetailを押した時の処理
    /// </summary>
    private void OnClickSelectCharaDetail()
    {
        //TODO アニメ演出

        //タップしたSelectCharaDetailの情報をポップアップに送る
        placementCharaSelectPop.SetSelectCharaDetail(charaData);
    }

    /// <summary>
    /// ボタンを押せる状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void ChangeActivateButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    public bool JudgePermissionCost(int value)
    {
        Debug.Log("コスト確認");

        //コストが支払える場合
        if (charaData.cost <= value)
        {
            ChangeActivateButton(true);
            return true;
        }
        return false;  //if文がfalseになった時、この処理が行われる
    }

    /// <summary>
    /// ボタンの状態の取得(今後のために実装)
    /// </summary>
    /// <returns></returns>
    public bool GetActivateButtonState()
    {
        return btnSelectCharaDetail.interactable;
    }

    /// <summary>
    /// CharaDataの取得(今後のために実装)
    /// </summary>
    /// <returns></returns>
    public CharaData GetCharaData()
    {
        return charaData;
    }
}
