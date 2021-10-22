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

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("Bouncer Helper")]
    [SerializeField] private BounceHelper _bounceHelper;

    private bool _invencible;
    private bool _canRun;
    private float _currentSpeed;
    private float _baseSpeedToAnimation = 7f;
    private Vector3 _pos;
    private Vector3 _startPosition;

    private void Start()
    {
        transform.localScale = Vector3.zero;

        ResetSpeed();
        _startPosition = transform.position;
        _invencible = false;

        transform.DOScale(1, 3f).SetEase(Ease.OutBack).SetDelay(.6f);
    }

    // Update is called once per frame
    private void Update()
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
            Debug.Log("End Game");
            EndGame(AnimatorManager.AnimationType.DEAD);
            MoveBack();
        } 
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == tagToCheckEndLine)
        {
            Debug.Log("End Game");
            EndGame();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    public void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endGameScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
        Debug.Log("STARTOU");
    }

    #region POWERUPS

    public void SetInvencible(bool b = true)
    {
        _invencible = b;
        gameObject.GetComponent<BoxCollider>().isTrigger = b;
        Bounce(1.2f);
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
        Bounce(1.2f);
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Bounce(1.2f);
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

    public void Bounce(float scale = 0f)
    {
        if (_bounceHelper != null)
        {
            if (scale != 0f)_bounceHelper.Bounce(scale);
            else _bounceHelper.Bounce();
        }
    }
}
