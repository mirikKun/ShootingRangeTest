using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Timer timer;
    [SerializeField] private LayerMask mask;
    private float _maxDistanceToCheck = 50f;

    //Запускает таймер после взгляда на особую мишень
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, _maxDistanceToCheck, mask))
        {
            Debug.Log("aaa");
            timer.StartTimer();
        }
    }
}