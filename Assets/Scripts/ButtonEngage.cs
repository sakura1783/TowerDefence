using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEngage : MonoBehaviour
{
    [SerializeField] private Button btnEngage;

    [SerializeField] private EngageManager engageManager;

    [SerializeField] private PlacementEngageCharaPopUp placementEngageCharaPopUp;

    public void SetUpButtonEngage()
    {
        btnEngage.onClick.AddListener(OnClickBtnEngage);

        Debug.Log("buttonEngageの設定完了");
    }

    /// <summary>
    /// btnEngageを押した際の処理
    /// </summary>
    public void OnClickBtnEngage()
    {
        //placementEngageCharaPopUp.ShowPopUp();

        engageManager.ActivatePlacementEngageCharaPopUp();

        Debug.Log("OnClickBtnEngage");
    }
}
