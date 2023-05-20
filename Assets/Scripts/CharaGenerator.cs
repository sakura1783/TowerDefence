    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class CharaGenerator : MonoBehaviour
    {
        //[SerializeField]
        //private GameObject charaPrefab;

        [SerializeField] CharaController charaControllerPrefab;

        [SerializeField]
        private Grid grid;

        [SerializeField]
        private Vector3Int gridPos;

        [SerializeField]
        private Tilemap tilemaps;

        [SerializeField] private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;

        [SerializeField] private Transform canvasTran;

        [SerializeField, Header("キャラのデータリスト")] private List<CharaData> charaDatasList = new List<CharaData>();

        private PlacementCharaSelectPopUp placementCharaSelectPopUp;

        private GameManager gameManager;

        void Update()
        {
            if (gameManager.GetPlacementCharaCount() >= GameData.instance.macCharaPlacementCount)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf && gameManager.currentGameState == GameManager.GameState.Play)  //activeSelfはオブジェクトの表示、非表示の情報がtrue,falseで返される
            {
                gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                    if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
                {
                    //CreateChara(gridPos);   //タップしてもすぐにキャラの生成を行わないように、コメントアウト

                    //配置キャラ選択用ポップアップの表示
                    ActivatePlacementCharaSelectPopUp();
                }
            }
        }

        /// <summary>
        /// キャラ生成
        /// </summary>
        /// <param name="gridPos"></param>
        //private void CreateChara(Vector3Int gridPos)
        //{
            //GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

            //chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        //}

        /// <summary>
        /// 設定
        /// </summary>
        /// <param name="gameManager"></param>
        /// <returns></returns>
        public IEnumerator SetUpCharaGenerator(GameManager gameManager, MapInfo currentMapInfo)
        {
            Debug.Log("SetUpCharaGeneratorが動きます");
            this.gameManager = gameManager;

            //grid変数にそれぞれのステージのBase側のgridの情報を代入する
            grid = currentMapInfo.GetMapInfo().Item2;

            //tilemaps変数にそれぞれのステージのtilemapの情報を代入する
            tilemaps = currentMapInfo.GetMapInfo().Item1;

            //TODO ステージのデータを取得

            //キャラのデータをリスト化
            CreateHaveCharaDatasList();

            yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
        }

        /// <summary>
        /// 配置キャラ選択用ポップアップ生成
        /// </summary>
        /// <returns></returns>
        private IEnumerator CreatePlacementCharaSelectPopUp()
        {
            //ポップアップを生成
            placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);  //第3引数は、ローカル座標にするか、ワールド座標にするかの設定。trueにしたらどうなる？

            //ポップアップの設定。キャラ用設定用の情報も渡す
            placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);  //thisを渡すことで、SetUpPlacementCharaSelectPopUpメソッドの中で初期化ができるようになる。

            placementCharaSelectPopUp.gameObject.SetActive(false);

            yield return null;
        }

        /// <summary>
        /// 配置キャラ選択用のポップアップの表示
        /// </summary>
        public void ActivatePlacementCharaSelectPopUp()
        {
            //ゲームの進行状態をゲーム停止に変更
            gameManager.SetGameState(GameManager.GameState.Stop);

            //全ての敵の移動を一時停止
            gameManager.PauseEnemies();

            placementCharaSelectPopUp.gameObject.SetActive(true);
            placementCharaSelectPopUp.ShowPopUp();
        }

        public void InactivatePlacementCharaSelectPopUp()
        {
            placementCharaSelectPopUp.gameObject.SetActive(false);

            //ゲームオーバーやゲームクリアではない場合
            if (gameManager.currentGameState == GameManager.GameState.Stop)
            {
                //ゲームの進行状態をプレイ中に変更して、ゲーム再開
                gameManager.SetGameState(GameManager.GameState.Play);

                //全ての敵の移動を再開
                gameManager.ResumeEnemies();

                //カレンシーの加算処理を再開
                StartCoroutine(gameManager.TimeToCurrency());
            }
        }

        /// <summary>
        /// キャラのデータをリスト化
        /// </summary>
        private void CreateHaveCharaDatasList()
        {
            //CharaDataSOスクリプタブル・オブジェクト内のCharaDataを一つずつリストに追加
            //TODO スクリプタブル・オブジェクトではなく、実際にプレイヤーが所持しているキャラの番号をもとにキャラのデータのリストを作成
            for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
            {
                charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
            }
        }

        /// <summary>
        /// 選択したキャラを生成して配置
        /// </summary>
        /// <param name="chooseCharaData"></param>
        public void CreateChooseChara(CharaData charaData)
        {
            //コスト支払い
            GameData.instance.currency -= charaData.cost;

            //カレンシーの画面表示を更新
            gameManager.uiManager.UpdateDisplayCurrency();

            CharaController chara = Instantiate(charaControllerPrefab, gridPos, Quaternion.identity);

            //位置が左下を0, 0としているので、中央にくるように調整
            chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);

            //キャラの設定
            chara.SetUpChara(charaData, gameManager);

            Debug.Log(charaData.charaName);

            //キャラをリストに追加
            gameManager.AddCharasList(chara);
        }
    }
