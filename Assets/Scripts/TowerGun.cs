using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGun : MonoBehaviour
{
    ParticleSystem.EmissionModule emission;
    Tower tower = null;

    private bool hasTarget = false;

    void Start()
    {
        tower = GetComponent<Tower>();
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        emission = particleSystem.emission;
        emission.rateOverTime = tower.shotsPerSecond;
    }

    private void Update()
    {
        hasTarget = tower.HasTarget();
        emission.enabled = hasTarget;
    }

    public float GetDamage()
    {
        return tower.damagePerShot;
    }

    public void SetRotation(Quaternion toRotation)
    {
        transform.rotation = toRotation;
    }
}
