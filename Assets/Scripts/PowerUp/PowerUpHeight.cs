using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHeight : PowerUpBase
{
    [Header("Height")]
    public float amountToIncrease = 2;
    public float animationDuration = .1f;
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountToIncrease, duration, animationDuration, ease);
    }
}
