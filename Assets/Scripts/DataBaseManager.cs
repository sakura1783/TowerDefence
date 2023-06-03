using UnityEngine;
using System.Collections.Generic;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public CharaDataSO charaDataSO;
    public AttackRangeSizeSO attackRangeSizeSO;
    public EnemyDataSO enemyDataSO;
    public StageDataSO stageDataSO;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 GetAttackRangeSize(AttackRangeType attackRangeType)
    {
        return attackRangeSizeSO.attackRangeSizeList.Find(x => x.attackRangeType == attackRangeType).size;
    }

    public List<CharaData> GetCharaDataList()
    {
        return charaDataSO.charaDatasList;
    }

    public StageData GetStageData(int stageNo)
    {
        //foreach (StageData stageData in stageDataSO.stageDatasList)
        //{
            //if (stageData.stageNo == stageNo)
            //{
                //return stageData;
            //}
        //}
        //return null;

        return stageDataSO.stageDatasList.Find(stageData => stageData.stageNo == stageNo);
    }
}
