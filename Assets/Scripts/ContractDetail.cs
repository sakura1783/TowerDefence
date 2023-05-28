using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 契約演出用のクラス
/// </summary>
public class ContractDetail : MonoBehaviour
{
    [SerializeField] private Image imgChara;

    [SerializeField] private Text txtCharaName;

    [SerializeField] private Button btnSubmitContractStamp;
    [SerializeField] private Button btnFilter;

    [SerializeField] private CanvasGroup canvasGroupContractSet;
    [SerializeField] private CanvasGroup canvasGroupSubmitContractStamp;

    [SerializeField] private Image imgContractStamp;

    /// <summary>
    /// 契約演出の設定
    /// </summary>
    /// <param name="charaData"></param>
    public void SetUpContractDetail(CharaData charaData)
    {
        imgChara.sprite = charaData.charaSprite;
        txtCharaName.text = charaData.charaName;

        canvasGroupContractSet.alpha = 0;

        //順番にボタンを押せるように、後から表示するスタンプの画像を見えないように設定し、タップ感知しないようにしておく
        canvasGroupSubmitContractStamp.alpha = 0;
        canvasGroupSubmitContractStamp.blocksRaycasts = false;  //blocksRaycastsがtrueの時、UI要素はイベントをブロックし、その要素上でのマウスクリックやタッチなどのイベントが他のUI要素に伝えられない。
        imgContractStamp.enabled = false;

        //各ボタンにメソッドを登録
        btnFilter.onClick.AddListener(OnClickFilter);
        btnSubmitContractStamp.onClick.AddListener(OnClickSubmitContract);

        // 0にしておいた、最初のボタン用にCanvasGroupを表示
        canvasGroupContractSet.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// スタンプ前にタップした際の処理
    /// </summary>
    private void OnClickFilter()
    {
        //スタンプを動かす、スタンプを3倍にする(サイズの大きくなっているものを小さくする演出に使うため)
        imgContractStamp.transform.localScale = Vector3.one * 3;

        //スタンプの角度をランダムに設定する(捺印の位置が毎回変わるようにする演出)
        imgContractStamp.transform.eulerAngles = new Vector3(0, 0, Random.Range(-30.0f, 30.0f));  //eulerAnglesで指定した座標に変換する

        //スタンプの画像を表示
        imgContractStamp.enabled = true;

        canvasGroupSubmitContractStamp.alpha = 1.0f;

        //スタンプをもとの大きさに戻す、Easeの設定により、もとのサイズに戻ってから少しだけ大きくして戻すことで、スタンプを捺しているように見せる
        imgContractStamp.transform.DOScale(Vector3.one, 0.75f)
            .SetEase(Ease.OutBack, 1.0f)
            .OnComplete(() =>
            {
                //ボタン(画面)を押せるようにする
                canvasGroupSubmitContractStamp.blocksRaycasts = true;
            });
    }

    /// <summary>
    /// スタンプ後にタップした際の処理
    /// </summary>
    private void OnClickSubmitContract()
    {
        //契約演出を終了して、ポップアップも閉じる
        canvasGroupContractSet.DOFade(0.0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); });

        GameData.instance.SaveEngageCharaList();
    }
}
