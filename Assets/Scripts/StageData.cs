using UnityEngine;

[System.Serializable]
public class StageData
{
    public string stageName;
    public int stageNo;
    public int generateIntervalTime;

    public int clearPoint;  //ステージクリア時のボーナスポイント
    public int defenceBaseLife;

    public Sprite stageSprite;

    public MapInfo mapInfo;

    public int maxCharaPlacementCount;

    //TODO 他にもあれば追加する
}
