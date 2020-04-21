using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tower : MonoBehaviour
{

    [SerializeField] Transform objectToPan = null;
    [SerializeField] Transform targetEnemy = null;
    [SerializeField] float targetDistance = 300f;
    [SerializeField] float towerAimSpeed = 1f;

    private bool hasTarget;

    private void Start()
    {
        objectToPan = GetComponentInChildren<ParticleSystem>().transform.parent;
    }

    void LateUpdate()
    {
        TargetEnemy();
    }

    private void TargetEnemy()
    {
        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(objectToPan.position, targetEnemy.position);
            if (distance <= targetDistance)
            {
                AimAtEnemy();
            }
            else
            {
                ResetAim();
            }
        }
        else
        {
            ResetAim();
        }
    }

    private void ResetAim()
    {
        hasTarget = false;
        objectToPan.rotation = Quaternion.Lerp(objectToPan.rotation, Quaternion.identity, Time.deltaTime * towerAimSpeed);
    }

    private void AimAtEnemy()
    {
        hasTarget = true;
        Vector3 direction = targetEnemy.position - objectToPan.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        objectToPan.rotation = Quaternion.Lerp(objectToPan.rotation, toRotation, Time.deltaTime * towerAimSpeed);
    }

    public bool HasTarget()
    {
        return hasTarget;
    }
}
