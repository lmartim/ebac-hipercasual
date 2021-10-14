using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem thisParticleSystem;
    public float timeToHide = 3f;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null && graphicItem.activeSelf)
        {
            graphicItem.SetActive(false);
            OnCollect();
        }
    }

    protected virtual void Collect()
    {
        HideItens();
        Invoke("HideObject", timeToHide);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (thisParticleSystem != null) thisParticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}
