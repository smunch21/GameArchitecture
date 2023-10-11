using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : TurretState
{
    private LazerTurret turret;
    private LineRenderer lineRenderer;

    

    public AttackState(LazerTurret _turret, LineRenderer _lineRenderer) :base(_turret)
    {
        turret = _turret;
        lineRenderer = _lineRenderer;
    }

    public override void OnStateEnter()
    {
        Debug.Log("Lazer hit: ");
        
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateUpdate()
    {
        ShootLazer();
        Vector3 lazerEnd = turret.shootPoint.position + turret.transform.forward * turret.maxLazerDistance;
        RaycastHit hit;
        if (Physics.Raycast(turret.shootPoint.position, turret.transform.forward, out hit, turret.maxLazerDistance)) 
        {
            lazerEnd = hit.point;

            Health hitHealth = hit.collider.GetComponent<Health>();
            if (hitHealth != null)
            {
                hitHealth.DeductHealth(1.0f);
            }
        }

    }

    private void DrawRay(Vector3 startPos , Vector3 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
        
    }
    
    void ShootLazer()
    {
        RaycastHit _hit;

        if (Physics.Raycast(turret.shootPoint.position, turret.shootPoint.forward, out _hit, turret.maxLazerDistance))
        {
            /*Vector3 localHitPoint = turret.transform.InverseTransformPoint(_hit.point);
            Vector3 localEndpoint = Vector3.ClampMagnitude(localHitPoint, turret.maxLazerDistance);*/
            DrawRay(turret.shootPoint.localPosition, _hit.point.normalized);
        }
        else
        {
            DrawRay(turret.shootPoint.localPosition, turret.shootPoint.forward * turret.maxLazerDistance);
        }
    }
}
