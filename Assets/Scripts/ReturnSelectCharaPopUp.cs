using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReturnSelectCharaPopUp : MonoBehaviour
{
    [SerializeField] private Button btnSubmitReturnChara;

    [SerializeField] private Button btnClosePopUp;

    [SerializeField] private CanvasGroup canvasGroup;

    private CharaController charaController;
    private GameManager gameManager;

    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="charaController"></param>
    /// <param name="gameManager"></param>
    public void SetUpReturnSelectCharaPopUp(CharaController charaController, GameManager gameManager)
    {
        this.charaController = charaController;
        this.gameManager = gameManager;

        btnSubmitReturnChara.interactable = false;
        btnClosePopUp.interactable = false;

        btnSubmitReturnChara.onClick.AddListener(OnClickSubmitReturnChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        btnSubmitReturnChara.interactable = true;
        btnClosePopUp.interactable = true;
    }

    /// <summary>
    /// 配置解除する時選択
    /// </summary>
    public void OnClickSubmitReturnChara()
    {
        CloseReturnCharaPopUp(true);
    }

    /// <summary>
    /// 配置解除しない時選択
    /// </summary>
    public void OnClickClosePopUp()
    {
        CloseReturnCharaPopUp(false);
    }

    /// <summary>
    /// ポップアップを閉じて選択したボタンの結果をGameManagerに送る
    /// </summary>
    /// <param name="isReturnChara"></param>
    public void CloseReturnCharaPopUp(bool isReturnChara)
    {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameManager.JudgeReturnChara(isReturnChara, charaController);
                DestroyReturnSelectCharaPopUp();
            });
    }

    /// <summary>
    /// ポップアップを破棄
    /// </summary>
    private void DestroyReturnSelectCharaPopUp()
    {
        Destroy(gameObject);
    }


}
