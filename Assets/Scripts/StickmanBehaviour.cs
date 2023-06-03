using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem fxBlue;
    [SerializeField] private ParticleSystem fxRed;
    private float jumpDuration;
    private float jumpForce;
    private Animator animator;

    private void Start()
    {
        jumpDuration = GameSettings.Instance.GetJumpDuration();
        jumpForce = GameSettings.Instance.GetJumpForce();

        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagList.StickmanEnemy) && other.transform.parent.childCount > 0)
        {
            DestroyStickmans(other);
        }

        if (other.CompareTag(TagList.Ramp))
        {
            transform.DOJump(transform.position, jumpForce, 1, jumpDuration).SetEase(Ease.Flash).OnComplete(PlayerBehaviour.Instance.StickmanFormation);
        }

        if (other.CompareTag(TagList.Stair)) // allow to detect the collored stairs
        {
            transform.parent.parent = null; // for instance Tower_0
            transform.parent = null;    // stickman
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            animator.SetBool("isRunning", false);

            //if (!PlayerBehaviour.Instance.GetMoveTheCamera())
            //{
            //    PlayerBehaviour.Instance.SetMoveTheCamera(true);
            //}

            if (PlayerBehaviour.Instance.player.transform.childCount == 2)
            {
                //other.GetComponent<Renderer>().material.DOColor(new Color(0.4f, 0.98f, 0.65f), 0.5f).SetLoops(1000, LoopType.Yoyo).SetEase(Ease.Linear);

                Debug.LogError("Finish");
            }

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
