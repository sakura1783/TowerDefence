using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Create EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public List<EnemyData> enemyDatasList = new List<EnemyData>();

    [Serializable]
    public class EnemyData
    {
        public string enemyName;
        public int enemyNo;
        public int hp;
        public int attackPower;
        public int moveSpeed;
        public EnemyType enemyType;

        [Header("アイテムドロップ率")] public int itemDropRate;

        //TODO 他にもあれば追加
    }
}
