using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInfo : MonoBehaviour
{
    [SerializeField] private Tilemap tilemaps;

    [SerializeField] private Grid grid;

    [SerializeField] private Transform defenceBaseTran;

    /// <summary>
    /// 出現するエネミー1体分の情報用クラス
    /// </summary>
    [System.Serializable]
    public class AppearEnemyInfo
    {
        [Header("x = 敵の番号。-1ならランダム")] public int enemyNo;

        [Header("敵の出現地点のランダム化。trueならランダム")] public bool isRandomPos;

        public PathData enemyPathData;  //移動経路の情報
    }

    public AppearEnemyInfo[] appearEnemyInfos;  //複数の出現するエネミーの情報を管理するための配列

    /// <summary>
    /// マップの情報を取得
    /// </summary>
    /// <returns></returns>
    public (Tilemap, Grid) GetMapInfo()
    {
        return (tilemaps, grid);
    }

    /// <summary>
    /// 防衛拠点の情報を取得
    /// </summary>
    /// <returns></returns>
    public Transform GetDefenceBaseTran()
    {
        return defenceBaseTran;
    }
}
