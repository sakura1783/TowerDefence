using UnityEngine;

public class DrawPathLine : MonoBehaviour
{
    [SerializeField]
    private float startLineWidth = 0.5f;

    [SerializeField]
    private float endLineWidth = 0.5f;

    /// <summary>
    /// 経路用のライン生成
    /// </summary>
    /// <param name="drawPaths"></param>
    public void CreatePathLine(Vector3[] drawPaths)
    {
        TryGetComponent(out LineRenderer lineRenderer);

        // ラインの太さを調整
        lineRenderer.startWidth = startLineWidth;
        lineRenderer.endWidth = endLineWidth;

        // 生成するラインの頂点数を決定(今回は始点と終点を一つずつ)
        lineRenderer.positionCount = drawPaths.Length;  //これを書かないと、矢印がでない

        //ラインを一つ生成
        lineRenderer.SetPositions(drawPaths);
    }
}