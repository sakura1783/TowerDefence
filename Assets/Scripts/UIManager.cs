using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text txtCost;

    [SerializeField] private Transform canvasTran;

    [SerializeField] private ReturnSelectCharaPopUp returnSelectCharaPopUpPrefab;

    public void UpdateDisplayCurrency()
    {
        txtCost.text =  GameData.instance.currency.ToString();
    }

    /// <summary>
    /// キャラの配置を解除する選択用のポップアップの生成
    /// </summary>
    /// <param name="charaController"></param>
    /// <param name="gameManger"></param>
    public void CreateReturnCharaPopUp(CharaController charaController, GameManager gameManger)
    {
        ReturnSelectCharaPopUp returnSelectCharaPopUp = Instantiate(returnSelectCharaPopUpPrefab, canvasTran, false);
        returnSelectCharaPopUp.SetUpReturnSelectCharaPopUp(charaController, gameManger);
    }

    /// <summary>
    /// オープニング処理
    /// </summary>
    //public string Opening()
    //{
        //TODO オープニングの処理を追加する
    //}
}
