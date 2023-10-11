using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
   protected LazerTurret turret;

    public TurretState(LazerTurret _turret)
    {
        this.turret = _turret;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();

}
