using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float power = 10.0f;


    //Создает пулю и запускает её в направлении взгляда
    private void Update()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        GameObject newProjectile =
            Instantiate(projectile, muzzlePoint.position, Quaternion.identity);

        if (!newProjectile.GetComponent<Rigidbody>())
        {
            newProjectile.AddComponent<Rigidbody>();
        }

        newProjectile.GetComponent<Rigidbody>().AddForce(mainCamera.forward * power, ForceMode.VelocityChange);
    }
}