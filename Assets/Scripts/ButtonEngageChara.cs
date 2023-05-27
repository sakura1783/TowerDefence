using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEngageChara : MonoBehaviour
{
    [SerializeField] private Button btnEngageChara;

    [SerializeField] private GameObject contractSet;

    [SerializeField] private Transform canvasOverleyTran;

    [SerializeField] private ContractDetail contractDetail;

    [SerializeField] private CharaData charaData;

    /// <summary>
    /// 設定
    /// </summary>
    public void SetUpBtnEngageChara()
    {
        btnEngageChara.onClick.AddListener(OnClickBtnEngageChara);
    }

    private void OnClickBtnEngageChara()
    {
        Instantiate(contractSet, canvasOverleyTran);

        //SetUpContractDetailメソッドを実行する(生成する位置と情報については、変数に登録したものを使う)
        contractDetail.SetUpContractDetail(charaData);  //TODO どうやってSetUpメソッドに情報を渡せばいいのかわからない
    }
}
