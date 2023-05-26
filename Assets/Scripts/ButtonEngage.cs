using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEngage : MonoBehaviour
{
    [SerializeField] private Button btnEngage;

    [SerializeField] private EngageManager engageManager;

    [SerializeField] private PlacementEngageCharaPopUp placementEngageCharaPopUp;

    public void OnClick()
    {
        btnEngage.onClick.AddListener(OnClickBtnEngage);
    }

    /// <summary>
    /// btnEngageを押した際の処理
    /// </summary>
    public void OnClickBtnEngage()
    {
        engageManager.SetUpEngageManager();

        placementEngageCharaPopUp.ShowPopUp();

        Debug.Log("OnClickBtnEngage");
    }
}
