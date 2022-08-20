using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBehavior))]
public class FovEditor : Editor
{
    private void OnSceneGUI()
    {
        // Circle Fow
        EnemyBehavior fow = (EnemyBehavior)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.currentRadius); 
        // Circle AttackRng
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.currentAttackRange);

        // Create Angle Points On Radius
        Vector3 viewAngleA = fow.DirectionFromAngle(-fow.currentViewAngle / 2, false);
        Vector3 viewAngleB = fow.DirectionFromAngle(fow.currentViewAngle / 2, false);
        
        Handles.color = Color.white;
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.currentRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.currentRadius);

        Handles.color = Color.red;
        foreach (Transform visableTarget in fow.targetsAquired)
        {
            Handles.DrawLine(fow.transform.position, visableTarget.position); 
        }

        Handles.color = Color.black;
        foreach(Transform targetsInRange in fow.targetInAttRange)
        {
            Handles.DrawLine(fow.transform.position, targetsInRange.position);
        }
    }
}
