using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 10f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endGameScreen;

    // private
    private bool _canRun;
    private Vector3 _pos;

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            EndGame();
        } else if (collision.transform.tag == tagToCheckEndLine)
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
}
