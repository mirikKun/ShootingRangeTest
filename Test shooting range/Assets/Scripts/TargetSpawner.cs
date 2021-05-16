using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private float range = 10;
    [SerializeField, Range(11, 30)] private int count = 12;
    [SerializeField] private Target target;
    [SerializeField] private Transform pointer;
    [SerializeField] private Text counter;
    [SerializeField] private float pointerSpeed = 0.5f;

    private Target[] _targets;
    private const float RiseHeight = 1;
    private int _movingStartIndex = 4;
    private int _currentTargetIndex = 0;
    private int _objectiveTargetCount = 10;
    private int _angleFromY = 10;


    //Создает все мишени и устаналивает особую
    private void Start()
    {
        Transform myTransform = transform;
        _targets = new Target[count];

        for (int i = 0; i < count; i++)
        {
            _targets[i] = Instantiate(target, GetSpawnPosition(myTransform.position, range), Quaternion.identity,
                myTransform);
        }

        SetCurrentTarget();
        _objectiveTargetCount = GameManager.GM.GetObjective();
    }

    //Задает следущую мишень и проверяет на колическтво уже сбитых
    private void SetCurrentTarget()
    {
        if (_currentTargetIndex >= _objectiveTargetCount)
        {
            GameManager.GM.Victory();
            return;
        }
        
        if (_currentTargetIndex == _movingStartIndex)
        {
            EnableTargetMoving();
        }
        else if (_currentTargetIndex > _movingStartIndex)
        {
            IncreaseTargetSpeed();
        }

        StopAllCoroutines();

        _targets[_currentTargetIndex].SetCurrentTarget();
        _targets[_currentTargetIndex].StartCountdown();
        StartCoroutine(WayToNewTarget(_targets[_currentTargetIndex].transform));
    }

    //Начинает движение мишеней после определенного момента
    private void EnableTargetMoving()
    {
        for (int i = _movingStartIndex; i < count; i++)
        {
            _targets[i].StartMoving();
        }
    }

    //Увеличивает скорость движение мишеней 
    private void IncreaseTargetSpeed()
    {
        for (int i = _movingStartIndex; i < count; i++)
        {
            _targets[i].IncreaseSpeed();
        }
    }

    //Увеличивает очки, переключает мишень
    public void OnTargetHit()
    {
        _currentTargetIndex++;
        counter.text = (_currentTargetIndex).ToString();
        SetCurrentTarget();
    }

    //Меняет мишень после определённого времени
    public void OnTargetTimePassed()
    {
        Target missedTarget = _targets[_currentTargetIndex];
        int newIndex = Random.Range(_currentTargetIndex, count);
        _targets[_currentTargetIndex] = _targets[newIndex];
        _targets[newIndex] = missedTarget;
        SetCurrentTarget();
    }

    //Запускает следующую за мишенями линию
    private IEnumerator WayToNewTarget(Transform newPosition)
    {
        while (newPosition)
        {
            pointer.position = Vector3.Slerp(pointer.position, newPosition.position, pointerSpeed);
            yield return null;
        }
    }

    //С помощью сферических координат генерирую положение для цели в полусфере
    private Vector3 GetSpawnPosition(Vector3 center, float radius)
    {
        float angXZ = Random.Range(0, 360);
        float angY = Random.Range(_angleFromY, 90);
        Vector3 spawnPos;
        spawnPos.x = center.x + radius * Mathf.Sin(angXZ * Mathf.Deg2Rad) * Mathf.Cos(angY * Mathf.Deg2Rad);
        spawnPos.y = center.z + RiseHeight + radius * Mathf.Sin(angY * Mathf.Deg2Rad);
        spawnPos.z = center.y + radius * Mathf.Cos(angXZ * Mathf.Deg2Rad) * Mathf.Cos(angY * Mathf.Deg2Rad);
        return spawnPos;
    }
}