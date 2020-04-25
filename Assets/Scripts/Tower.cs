using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tower : MonoBehaviour
{
    public Waypoint currentWaypoint;

    public float targetDistance = 30f;
    public float towerAimSpeed = 5f;
    public float damagePerShot = 10;
    public float shotsPerSecond = 5f;

    private bool hasTarget;
    float distToClosestEnemy;
    Transform targetEnemy;
    Transform objectToPan;

    GameObject deathFXtemplate;
    Pathfinder pathfinder;

    Vector3 startWaypointPosition;


    bool isDying = false;
    

    private void Start()
    {
        objectToPan = GetComponentInChildren<ParticleSystem>().transform.parent;
        pathfinder = FindObjectOfType<Pathfinder>();
        startWaypointPosition = pathfinder.GetStartWaypoint().transform.position + Vector3.up * 5f;
    }

    void LateUpdate()
    {
        if (!isDying)
        {
            FindClosestEnemy();
            TargetEnemy();
        }
        else
        {
            AimTurret(objectToPan.position + Vector3.forward, false);
        }

    }

    private void FindClosestEnemy()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        distToClosestEnemy = 0f;
        foreach (EnemyMovement enemy in enemies)
        {
            float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distToClosestEnemy == 0f || distToEnemy<distToClosestEnemy)
            {
                targetEnemy = enemy.transform;
                distToClosestEnemy = distToEnemy;
            }
        }
    }

    private void TargetEnemy()
    {
        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(objectToPan.position, targetEnemy.position);
            if (distance <= targetDistance)
            {
                AimTurret(targetEnemy.position, true);
            }
            else
            {
                AimTurret(startWaypointPosition, false);
            }
        }
        else
        {
            AimTurret(startWaypointPosition, false);
        }
    }

    private void AimTurret(Vector3 targetPosition, bool target)
    {
        hasTarget = target;
        Vector3 direction = targetPosition - objectToPan.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        objectToPan.rotation = Quaternion.Lerp(objectToPan.rotation, toRotation, Time.deltaTime * towerAimSpeed);
    }
    /*
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
    */

    public bool HasTarget()
    {
        return hasTarget;
    }

    public void SetDeathFX(GameObject deathFX)
    {
        deathFXtemplate = deathFX;
    }

    public IEnumerator DeathSequence(int objectNumber)
    {
        isDying = true;
        Text deathTimerText = GetComponentInChildren<Text>();
        yield return new WaitForSeconds(objectNumber / 10f);
        deathTimerText.text = "3";
        deathTimerText.enabled = true;
        yield return new WaitForSeconds(1f);
        deathTimerText.text = "2";
        yield return new WaitForSeconds(1f);
        deathTimerText.text = "1";
        yield return new WaitForSeconds(1f);
        deathTimerText.text = "0";
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject deathFX = Instantiate(deathFXtemplate, transform.position + new Vector3(0f, 4f, 0f), Quaternion.identity);
        var FXduration = deathFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(deathFX, FXduration);
    }
}
