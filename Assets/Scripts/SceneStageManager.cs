using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStageManager : MonoBehaviour
{
    public static SceneStageManager instance;

    [SerializeField] private Fade fade;  //FadeCanvasゲームオブジェクトをアサインするための変数

    [SerializeField, Header("フェードの時間")] private float fadeDuration = 1.0f;

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

    /// <summary>
    /// 引数で指定したシーンへのシーン遷移の準備。シーン遷移を実行する場合は、このメソッドを利用する
    /// </summary>
    /// <param name="nextSceneType"></param>
    public void PrepareNextScene(SceneType nextSceneType)
    {
        if (!fade)
        {
            //FadeCanvasの情報がない場合、今までと同じようにすぐにシーン遷移
            StartCoroutine(LoadNextScene(nextSceneType));
        }
        else
        {
            //FadeCanvasの情報がある場合、fadeDuration変数秒の時間をかけてフェードインの処理を行ってから、引数で指定したシーンへ遷移
            fade.FadeIn(fadeDuration, () => { StartCoroutine(LoadNextScene(nextSceneType)); });
        }
    }

    /// <summary>
    /// 引数で指定したシーンへ遷移
    /// </summary>
    /// <param name="nextLoadSceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadNextScene(SceneType nextLoadSceneName)
    {
        //シーン名を指定する引数には、enumであるSceneTypeの列挙子を、ToStringメソッドを使ってstring型へキャストして利用
        SceneManager.LoadScene(nextLoadSceneName.ToString());

        //フェードインしている場合には
        if (fade)
        {
            //読み込んだ新しいシーンの情報を取得
            Scene scene = SceneManager.GetSceneByName(nextLoadSceneName.ToString());

            //シーンの読み込み終了を待つ
            yield return new WaitUntil(() => scene.isLoaded);

            //シーンの読み込み完了してからフェードアウトして、場面転換を完了する
            fade.FadeOut(fadeDuration);
        }
    }
}
