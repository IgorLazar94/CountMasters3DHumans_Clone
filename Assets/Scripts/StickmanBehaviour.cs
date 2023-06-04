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
    private PlayerBehaviour playerBehaviour;

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
            DestroyStickmansFromEnemies(other);
        }

        if (other.CompareTag(TagList.Ramp))
        {
            transform.DOJump(transform.position, jumpForce, 1, jumpDuration).SetEase(Ease.Flash).OnComplete(PlayerBehaviour.Instance.StickmanFormation);
            playerBehaviour.HandOverAddCoin();
        }

        if (other.CompareTag(TagList.Stair))
        {
            transform.parent.parent = null;
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            animator.SetBool("isRunning", false);

            if (PlayerBehaviour.Instance.player.transform.childCount == 2)
            {
                other.gameObject.transform.parent.GetComponent<StairsManager>().MultiplyCoinsByFactor(other.transform);
                
            }
        }

        if (other.CompareTag(TagList.Obstacle))
        {
            DestroyStickmansFromObstacles();
            playerBehaviour.RemovePlayerStickman();
            //StartCoroutine(ReformationStickman());
        }
    }

    private IEnumerator ReformationStickman()
    {
        yield return new WaitForSecondsRealtime(2f);
        playerBehaviour.StickmanFormation();
    }

    public void SetPlayerBehaviour(PlayerBehaviour _playerBehaviour)
    {
        playerBehaviour = _playerBehaviour;
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

    private void DestroyStickmansFromEnemies(Collider enemyStickman)
    {
        PlayRandomFX();
        Destroy(enemyStickman.gameObject);
        Destroy(gameObject);
    }

    private void DestroyStickmansFromObstacles()
    {
        PlayBlueFX();
        Destroy(gameObject);
    }

    private void PlayBlueFX()
    {
        Instantiate(fxBlue, transform.position, Quaternion.identity);
    }
}
