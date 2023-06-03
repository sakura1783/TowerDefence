using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeGauge : MonoBehaviour
{
    private int maxHp;

    private int hp;

    [SerializeField] private Slider lifeGaugeSlider;

    [SerializeField] private EnemyController enemyController;

    //void Start()
    //{
        //maxHp = GameData.instance.defenceBaseLife;
        //hp = maxHp;
        //lifeGaugeSlider.value = hp;

        //Debug.Log("maxHpの値 : " + maxHp);
        //Debug.Log("valueの値 : " + lifeGaugeSlider.value);
    //}

    /// <summary>
    /// LifeGaugeの設定
    /// </summary>
    /// <param name="hp"></param>
    public void SetUpLifeGauge(int hp)
    {
        maxHp = hp;
        this.hp = maxHp;
        lifeGaugeSlider.maxValue = this.hp;
        lifeGaugeSlider.value = this.hp;
    }

    /// <summary>
    /// DefenceBaseに侵入された際のライフゲージを減らす処理
    /// </summary>
    public void ReduceLifeGauge(int hp)
    {
        //hp = hp - enemyController.attackPoint;
        lifeGaugeSlider.DOValue(hp, 0.25f).SetEase(Ease.Linear);
    }
}
