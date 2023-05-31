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

    void Start()
    {
        maxHp = GameData.instance.defenceBaseLife;
        hp = maxHp;
        lifeGaugeSlider.value = maxHp;
    }

    /// <summary>
    /// DefenceBaseに侵入された際のライフゲージを減らす処理
    /// </summary>
    public void ReduceLifeGauge()
    {
        hp = hp - enemyController.attackPoint;
        lifeGaugeSlider.DOValue(hp, 0.25f).SetEase(Ease.Linear);
    }
}
