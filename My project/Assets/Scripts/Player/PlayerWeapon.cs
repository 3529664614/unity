using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWeapon
{
    public string name = "M16A1";
    public int damage = 10;
    public float range = 100f;
    public float shootRate = 10f;
    public float shootCoolDownTime = 0.75f; //单发模式冷却时间
    public float recoilForce = 2f; //后坐力

    public int maxBullets = 30;
    public int bullets = 30;
    public float reloadTime = 2f;
    [HideInInspector]
    public bool isReloading = false;
    public GameObject graphics;
}