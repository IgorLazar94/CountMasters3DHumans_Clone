using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Range(0f, 1f)][SerializeField] float distanceBetween;
    [Range(0f, 1f)][SerializeField] float radius;
    [SerializeField] private GameObject stickMan;
    [SerializeField] private TextMeshPro counterText;
    [SerializeField] private InputController inputController;
    [HideInInspector] public Transform player;
    private bool isAttacking;
    private float distanceToAttack;
    [SerializeField] private int numberOfEnemies;
    private void Start()
    {
        distanceToAttack = GameSettings.Instance.GetDistanceToAttack();

        CreateEnemies();
        UpdateLabelText();
        StickmanFormation();
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var enemy = Instantiate(stickMan, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
            enemy.gameObject.tag = TagList.StickmanEnemy;
        }
    }

    public void UpdateLabelText()
    {
        counterText.text = (transform.childCount - 1).ToString();
    }

    private void StickmanFormation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = distanceBetween * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceBetween * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            Vector3 newPos = new Vector3(x, -1f, z);

            transform.transform.GetChild(i).localPosition = newPos;
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking && transform.childCount > 1)
        {
            var enemyDirection = player.position - transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up),
                    Time.deltaTime * 3f);

                if (player.childCount > 1)
                {
                    CalculateDistance(i);
                }
            }
        }
    }

    private void CalculateDistance(int childEnemy)
    {
        var distance = player.GetChild(1).position - transform.GetChild(childEnemy).position;

        if (distance.magnitude < distanceToAttack)
        {
            transform.GetChild(childEnemy).position = Vector3.Lerp(transform.GetChild(childEnemy).position, player.GetChild(1/*childEnemy*/).position, Time.deltaTime * 2f);
        }
    }

    public void EnemyAttack(Transform playerPos)
    {
        player = playerPos;
        isAttacking = true;

        SwitchAnimator(true);
    }

    public void StopAttacking()
    {
        inputController.SetGameState(false);
        isAttacking = false;

        SwitchAnimator(false);
    }

    private void SwitchAnimator(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("isRunning", value);
        }
    }
}
