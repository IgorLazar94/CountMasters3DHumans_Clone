using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem fxBlue;
    [SerializeField] private ParticleSystem fxRed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagList.StickmanEnemy) && other.transform.parent.childCount > 0)
        {
            DestroyStickmans(other);
        }

        if (other.CompareTag(TagList.Ramp))
        {
            transform.DOJump(transform.position, 1f, 1, 1f).SetEase(Ease.Flash).OnComplete(PlayerBehaviour.Instance.StickmanFormation);
        }


    }

    private void PlayRandomFX()
    {
        int random = Random.Range(0, 2);
        if (random > 0)
        {
            Instantiate(fxBlue, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(fxBlue, transform.position, Quaternion.identity);
        }

    }

    private void DestroyStickmans(Collider enemyStickman)
    {
        PlayRandomFX();
        Destroy(enemyStickman.gameObject);
        Destroy(gameObject);
    }
}
