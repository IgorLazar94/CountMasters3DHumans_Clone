using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerBehaviour : MonoBehaviour
{
    private TextMeshPro counterText;
    [SerializeField] private GameObject stickMan;
    public Transform player;
    private int stickmansCount;
    private InputController inputController;
    // ====================================

    [Range(0f, 1f)] [SerializeField] float distanceBetween;
    [Range(0f, 1f)] [SerializeField] float radius;

    [SerializeField] private Transform enemy;
    private bool isAttack;

    private void Start()
    {
        inputController = gameObject.GetComponent<InputController>();
        player = transform;
        counterText = GetComponentInChildren<TextMeshPro>();
        stickmansCount = transform.childCount - 1;

        counterText.text = stickmansCount.ToString();
    }

    private void Update()
    {
        if (isAttack)
        {
            var enemyDirection = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z) - transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, 
                                                                  Quaternion.LookRotation(enemyDirection, Vector3.up), 
                                                                  Time.deltaTime * 3f);
            }

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                    if (distance.magnitude < 5f)  // distance to attack player
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, 
                                                                new Vector3(enemy.GetChild(1).GetChild(0).position.x,
                                                                            transform.GetChild(i).position.y, 
                                                                            enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 1f);

                    }
                }
            }
            else
            {
                isAttack = false;
                float _roadSpeed = inputController.GetRoadSpeed();
                inputController.SetRoadSpeed(_roadSpeed * 2);

                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.identity, Time.deltaTime * 2f);
                }

                StickmanFormation();
                enemy.gameObject.SetActive(false);
            }
        }
    }

    private void StickmanFormation()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            var x = distanceBetween * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceBetween * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            Vector3 newPos = new Vector3(x, -1f, z);

            player.transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);
        }
    }




    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(stickMan, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        }

        stickmansCount = transform.childCount - 1;
        counterText.text = stickmansCount.ToString();

        StickmanFormation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagList.Gate))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            var gateScript = other.GetComponent<Gate>();

            if (gateScript.gateType == GateType.Multiply)
            {
                MakeStickman(stickmansCount * gateScript.randomNumber);
            }
            else
            {
                MakeStickman(stickmansCount + gateScript.randomNumber);
            }
        }

        if (other.CompareTag(TagList.Enemy))
        {
            enemy = other.transform;
            isAttack = true;

            float _roadSpeed = inputController.GetRoadSpeed();
            inputController.SetRoadSpeed(_roadSpeed / 2);

            other.transform.GetChild(1).GetComponent<EnemyController>().EnemyAttack(transform);
        }
    }

    public int GetStickmansCount()
    {
        return stickmansCount;
    }

    public bool GetIsAttackPlayer()
    {
        return isAttack;
    }

}
