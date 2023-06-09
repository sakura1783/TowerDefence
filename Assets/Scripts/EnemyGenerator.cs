using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyPrefab;

    [SerializeField]
    private PathData[] pathDatas;

    [SerializeField]
    private DrawPathLine pathLinePrefab;

    private GameManager gameManager;

    private StageData stageData;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// 敵の生成準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrepareEnemyGenerate(GameManager gameManager, StageData stageData)
    {
        this.stageData = stageData;
        this.gameManager = gameManager;

        int timer = 0;

        while (gameManager.isEnemyGenerate)
        {
            timer++;

            if (this.gameManager.currentGameState == GameManager.GameState.Play)
            {
                if (timer > gameManager.generateIntervalTime)
                {
                    timer = 0;

                    //GenerateEnemy();

                    gameManager.AddEnemyList(GenerateEnemy());

                    gameManager.JudgeGenerateEnemysEnd();
                }
            }
            yield return null;
        }

        // TODO 生成終了後の処理を記述する
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    /// <param name="generateNo"></param>
    /// <returns></returns>
    public EnemyController GenerateEnemy(int generateNo = 0)
    {
        //int randomValue = Random.Range(0, pathDatas.Length);

        //EnemyController enemyController = Instantiate(enemyPrefab, pathDatas[randomValue].generateTran.position, Quaternion.identity);

        //移動する地点を取得
        //Vector3[] paths = pathDatas[randomValue].pathTranArray.Select(x => x.position).ToArray();  //xはpathData.pathTranArrayの各要素、x.positionはその各要素のpositionプロパティを表す。それをToArrayで配列に格納している

        //int enemyNo = Random.Range(0, DataBaseManager.instance.enemyDataSO.enemyDatasList.Count);

        //生成位置(基本的にはElementの番号と同じ。-1の場合はランダム)
        int posNo = generateNo;

        //生成位置がランダムか確認
        if (stageData.mapInfo.appearEnemyInfos[generateNo].isRandomPos)
        {
            posNo = Random.Range(0, stageData.mapInfo.appearEnemyInfos.Length);
        }

        //敵の生成
        EnemyController enemyController = Instantiate(enemyPrefab, stageData.mapInfo.appearEnemyInfos[posNo].enemyPathData.generateTran.position, Quaternion.identity);

        //敵の種類
        int enemyNo = stageData.mapInfo.appearEnemyInfos[generateNo].enemyNo;

        //敵がランダムか確認
        if (stageData.mapInfo.appearEnemyInfos[generateNo].enemyNo == -1)
        {
            //エラーになったのでintは自分でつけたよ
            enemyNo = Random.Range(0, DataBaseManager.instance.enemyDataSO.enemyDatasList.Count);
        }

        Vector3[] paths = stageData.mapInfo.appearEnemyInfos[posNo].enemyPathData.pathTranArray.Select(x => x.position).ToArray();

        //敵キャラの初期設定を行い、移動を一時停止しておく
        enemyController.SetUpEnemyController(paths, gameManager, DataBaseManager.instance.enemyDataSO.enemyDatasList[enemyNo]);

        //敵の移動経路のライン表示を生成の準備
        StartCoroutine(PrepareCreatePathLine(paths, enemyController));

        return enemyController;
    }

    private IEnumerator PrepareCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {
        yield return StartCoroutine(CreatePathLine(paths));

        yield return new WaitUntil(() => gameManager.currentGameState == GameManager.GameState.Play);

        enemyController.ResumeMove();
    }

    /// <summary>
    /// 移動経路用のラインの生成と破棄
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    private IEnumerator CreatePathLine(Vector3[] paths)
    {
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();

        for (int i = 0; i < paths.Length - 1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity);

            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };

            drawPathLine.CreatePathLine(drawPaths);

            drawPathLinesList.Add(drawPathLine);

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < drawPathLinesList.Count; i++)
        {
            Destroy(drawPathLinesList[i].gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// ステージに応じたpathDatasをセット
    /// </summary>
    /// <param name="pathDatas"></param>
    public void SetUpPathDatas(PathData[] pathDatas)
    {
        this.pathDatas = new PathData[pathDatas.Length];
        this.pathDatas = pathDatas;
    }
}
