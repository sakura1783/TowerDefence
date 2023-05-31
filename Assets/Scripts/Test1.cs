using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    [SerializeField] Text txtTotalClearPoint;

    public void DisplayTotal()
    {
        txtTotalClearPoint.text = GameData.instance.totalClearPoint.ToString();
    }
}
