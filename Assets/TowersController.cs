using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    [SerializeField] Tower tower1 = null;

    public Tower GetTower1()
    {
        return tower1;
    }
}
