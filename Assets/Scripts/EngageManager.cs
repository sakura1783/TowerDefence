using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageManager : MonoBehaviour
{
    [SerializeField] private PlacementEngageCharaPopUp placementEngageCharaPopUpPrefab;

    [SerializeField] private Transform canvasTran;

    [SerializeField] private List<CharaData> charaDatasList = new List<CharaData>();

    private PlacementEngageCharaPopUp placementEngageCharaPopUp;

    /// <summary>
    /// 設定
    /// </summary>
    /// <returns></returns>
    public void SetUpEngageManager()
    {
        CreatePlacementEngageCharaPopUp();
    }

    /// <summary>
    /// キャラ契約ポップアップ生成
    /// </summary>
    /// <returns></returns>
    private void CreatePlacementEngageCharaPopUp()
    {
        placementEngageCharaPopUp = Instantiate(placementEngageCharaPopUpPrefab, canvasTran, false);

        placementEngageCharaPopUp.SetUpPlacementEngageCharaPopUp(this, charaDatasList);

        placementEngageCharaPopUp.gameObject.SetActive(false);
    }

    public void InactivatePlacementEngageCharaPopUp()
    {
        placementEngageCharaPopUp.gameObject.SetActive(false);
    }
}
