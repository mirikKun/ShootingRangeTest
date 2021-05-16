using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 100f;
    [SerializeField] private float mouseSensitivityY = 100f;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int inversion = -1;

    private float _yRotation = 0f;

    private void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityX * Time.deltaTime;
        _yRotation += inversion * mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -90, 90f);
        mainCamera.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityY * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}