using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{

    [Header("Lerp")]
    public float lerpSpeed = 1f;
    public Transform target;

    [Header("Physics")]
    public float speed = 10f;

    [Header("Tags")]
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    [Header("UI")]
    public GameObject endGameScreen;

    [Header("Collectors")]
    public GameObject coinCollector;

    [Header("Privates")]
    private bool _invencible;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _pos;
    private Vector3 _startPosition;


    private void Start()
    {
        ResetSpeed();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy && !_invencible)
        {
            EndGame();
        } 
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == tagToCheckEndLine)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        _canRun = false;
        endGameScreen.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
    }

    #region POWERUPS

    public void SetInvencible(bool b = true)
    {
        _invencible = b;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
