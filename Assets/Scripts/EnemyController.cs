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

    public Transform enemy;
    public bool isAttacking;

    private void Start()
    {


        CreateEnemies();
        UpdateLabelText();
        StickmanFormation();
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < Random.Range(20, 120); i++)
        {
            var enemy = Instantiate(stickMan, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
            enemy.gameObject.tag = TagList.StickmanEnemy;
        }
    }

    private void UpdateLabelText()
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

    private void Update()
    {
        if (isAttacking && transform.childCount > 1)
        {
            var enemyPos = new Vector3(enemy.position.x, transform.position.y, enemy.position.z);
            var enemyDirection = /*enemyPos*/enemy.position - transform.position;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up),
                    Time.deltaTime * 3f);

                if (enemy.childCount > 1)
                {
                    var distance = enemy.GetChild(1).position - transform.GetChild(i).position;

                    if (distance.magnitude < 5f) // distance to attack
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, enemy.GetChild(i).position, Time.deltaTime * 2f);
                    }
                }
            }
        }
    }

    public void EnemyAttack(Transform enemyForce)
    {
        enemy = enemyForce;
        isAttacking = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("isRunning", true);
        }
    }

    public void StopAttacking()
    {
        inputController.SetGameState(false);
        isAttacking = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("isRunning", false);
        }
    }






}
