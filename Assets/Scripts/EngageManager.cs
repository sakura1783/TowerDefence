using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageManager : MonoBehaviour
{
    [SerializeField] private PlacementEngageCharaPopUp placementEngageCharaPopUpPrefab;

    [SerializeField] private Transform canvasTran;

    [SerializeField] private List<CharaData> charaDatasList = new List<CharaData>();

    private PlacementEngageCharaPopUp placementEngageCharaPopUp;

    [SerializeField] private ButtonEngage buttonEngage;

    private PlacementSelectStagePopUp placementSelectStagePopUp;

    [SerializeField] private WorldUIManager worldUiManager;

    void Start()
    {
        GameData.instance.LoadClearPoint();
        GameData.instance.LoadEngageCharaList();

        SetUpEngageManager();

        worldUiManager.UpdateTotalClearPoint();

        buttonEngage.SetUpButtonEngage();

        placementEngageCharaPopUp.SetUpBtnEngageChara();
    }

    /// <summary>
    /// 設定
    /// </summary>
    /// <returns></returns>
    public void SetUpEngageManager()
    {
        charaDatasList = DataBaseManager.instance.GetCharaDataList();

        CreatePlacementEngageCharaPopUp();
        CreatePlacementSelectStagePopUp();
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

    private void CreatePlacementSelectStagePopUp()
    {

    }

    public void InactivatePlacementEngageCharaPopUp()
    {
        placementEngageCharaPopUp.gameObject.SetActive(false);
    }

    public void ActivatePlacementEngageCharaPopUp()
    {
        placementEngageCharaPopUp.gameObject.SetActive(true);

        placementEngageCharaPopUp.ShowPopUp();
    }

    public void InactivatePlacementSelectStagePopUp()
    {
        placementSelectStagePopUp.gameObject.SetActive(false);
    }

    public void ActivatePlacementSelectStagePopUp()
    {
        placementSelectStagePopUp.gameObject.SetActive(true);

        placementSelectStagePopUp.ShowPopUp();
    }
}
