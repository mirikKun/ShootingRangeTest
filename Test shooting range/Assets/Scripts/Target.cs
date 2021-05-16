using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitExplosion;
    [SerializeField] private GameObject currentTargetLight;
    [SerializeField] private Material currentTargetMaterial;
    [SerializeField] private float speed = 15;
    [SerializeField] private float speedIncrease = 6;
    [SerializeField] private int currentTarget;

    private bool _isCurrentTarget;
    private Material _defaultMaterial;
    private Transform _myTransform;
    private float _timeToNewTarget = 10;
    private MeshRenderer _meshRenderer;
    private TargetSpawner _targetSpawner;

    private void Awake()
    {
        _targetSpawner = GetComponentInParent<TargetSpawner>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _myTransform = transform;
    }

    //Делает мишень особой, светящейся и целью для игрока
    public void SetCurrentTarget()
    {
        _isCurrentTarget = true;
        currentTargetLight.SetActive(true);
        _defaultMaterial = _meshRenderer.material;
        _meshRenderer.material = currentTargetMaterial;
        gameObject.layer = currentTarget;
    }

    
    //Начинает остчет для особой мишени чтоб особоая мишень поменялась
    public void StartCountdown()
    {
        StartCoroutine(SetDefaultTarget());
    }

    private IEnumerator  SetDefaultTarget()
    {
        yield return new WaitForSeconds(_timeToNewTarget);
        _isCurrentTarget = false;
        _meshRenderer.material = _defaultMaterial;
        currentTargetLight.SetActive(false);
        gameObject.layer = LayerMask.GetMask("Default");
        _targetSpawner.OnTargetTimePassed();
        yield return null;
    }

    
    //Начинает движение мишени после определенного момента
    public void StartMoving()
    {
        StartCoroutine(TargetMoving());
    }
    private IEnumerator TargetMoving()
    {
        int direction = Random.Range(0, 2) * 2 - 1;
        while (true)
        {
            _myTransform.RotateAround(new Vector3(0, _myTransform.position.y, 0), Vector3.up,
                direction * speed * Time.deltaTime);
            yield return null;
        }
    }

    public void IncreaseSpeed()
    {
        speed += speedIncrease;
    }
    //Если сталкиваеться с пулей, сообщает об этом и уничтожает мишень
    private void OnCollisionEnter(Collision other)
    {
        if (_isCurrentTarget && other.gameObject.GetComponent<Bullet>())
        {
            _targetSpawner.OnTargetHit();
            Destroy(Instantiate(hitExplosion, transform.position, Quaternion.identity), 3);
            Destroy(gameObject);
        }
    }
}