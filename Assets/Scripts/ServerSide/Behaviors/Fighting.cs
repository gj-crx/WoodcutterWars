using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting
{
    public Unit CurrentTarget;

    private Unit unit;
    private float timer_Hitting = 0;

    public Fighting(Unit unit)
    {
        this.unit = unit;
        unit.unitcontrolling += FightingControlling;
    }
    private void FightingControlling()
    {
        if (CurrentTarget != null)
        {
            Hit(CurrentTarget);
        }
    }

    private void Hit(Unit target)
    {
        if (Vector3.Distance(unit.position, target.position) < unit.AttackRange)
        {
            if (timer_Hitting > unit.AttackDelay)
            {
                timer_Hitting = 0;
                target.CurrentHP -= unit.Damage;
                if (target.CurrentHP <= 0)
                {
                    unit.onkill(target);
                    target.Death(unit);
                }
            }
            else
            {
                timer_Hitting += Time.deltaTime;
            }
        }
        else
        {
            timer_Hitting = 0;
        }
    }
}
