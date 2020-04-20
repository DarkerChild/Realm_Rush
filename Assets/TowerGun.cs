using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGun : MonoBehaviour
{
    [SerializeField] int damagePerShot = 20;
    //[SerializeField] float shotTime = 0.3f;

    ParticleSystem.EmissionModule emmision;
    Tower tower = null;

    private bool hasTarget = false;

    void Start()
    {
        tower = GetComponent<Tower>();
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        emmision = particleSystem.emission;
    }

    private void Update()
    {
        hasTarget = tower.HasTarget();
        emmision.enabled = hasTarget;
    }

    public int GetDamage()
    {
        return damagePerShot;
    }

    public void SetRotation(Quaternion toRotation)
    {
        transform.rotation = toRotation;
    }
}
