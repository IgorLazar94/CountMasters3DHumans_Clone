using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance;


    [SerializeField] private GameObject stickMan;
    [SerializeField] private CinemachineVirtualCamera secondCamera;
    private TextMeshPro counterLabelText;
    [HideInInspector] public Transform player;
    private int playerStickmansCount;
    private int enemyStickmansCount;
    private InputController inputController;
    // ====================================

    [Range(0f, 1f)] [SerializeField] float distanceBetween;
    [Range(0f, 1f)] [SerializeField] float radius;

    private Transform enemy;
    private bool isAttack;
    private bool moveTheCamera = false;
    private float distanceToAttack;

    private void Start()
    {
        distanceToAttack = GameSettings.Instance.GetDistanceToAttack();

        inputController = gameObject.GetComponent<InputController>();
        player = transform;
        player.GetChild(1).GetComponent<Animator>().SetBool("isRunning", true);
        counterLabelText = GetComponentInChildren<TextMeshPro>();
        playerStickmansCount = transform.childCount - 1;

        UpdateCounterText();

        Instance = this;
    }

    private void UpdateCounterText ()
    {
        counterLabelText.text = playerStickmansCount.ToString();
    }

    private void Update()
    {
        if (moveTheCamera && transform.childCount > 1)
        {
            var cinemachineTransposer = secondCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
            var cinemachineComposer = secondCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();

            //cinemachineTransposer.m_FollowOffset = new Vector3(4.5f,
            //                                                   Mathf.Lerp(cinemachineTransposer.m_FollowOffset.y,
            //                                                              transform.GetChild(1).position.y + 2f,
            //                                                              Time.deltaTime * 1f),
            //                                                   -5f);

            //cinemachineComposer.m_TrackedObjectOffset = new Vector3(0f, 
            //                                                       Mathf.Lerp(cinemachineComposer.m_TrackedObjectOffset.y, 
            //                                                                  4f,
            //                                                                  Time.deltaTime * 1f), 
            //                                                       0f);    



        }



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

                    if (distance.magnitude < distanceToAttack)
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
                DisableAttack();
                enemy.gameObject.SetActive(false);
            }

            if (transform.childCount == 1) // if blue == 0 () => StopAttack
            {
                enemy.transform.GetChild(1).GetComponent<EnemyController>().StopAttacking();
                gameObject.SetActive(false);
            } 


        }
    }

    private void DisableAttack()
    {
        isAttack = false;
        float _roadSpeed = inputController.GetRoadSpeed();
        inputController.SetRoadSpeed(_roadSpeed * 2);
        StickmanFormation();
        RotateForwardStickmans();
    }

    private void RotateForwardStickmans()
    {
        for (int i = 1; i < transform.childCount; i++)  
        {
            transform.GetChild(i).rotation = Quaternion.identity;
        }
    }

    public void StickmanFormation()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            var x = distanceBetween * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceBetween * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            Vector3 newPos = new Vector3(x, -1f, z);
            player.transform.GetChild(i).DOLocalMove(newPos, 0.5f).SetEase(Ease.OutBack); // Сократить кол-во вызовов!
        }
    }




    private void MakeStickman(int number)
    {
        for (int i = playerStickmansCount; i < number; i++)
        {
            Instantiate(stickMan, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        }

        playerStickmansCount = transform.childCount - 1;
        counterLabelText.text = playerStickmansCount.ToString();

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
                MakeStickman(playerStickmansCount * gateScript.randomNumber);
            }
            else
            {
                MakeStickman(playerStickmansCount + gateScript.randomNumber);
            }
        }

        if (other.CompareTag(TagList.Enemy))
        {
            enemy = other.transform;
            isAttack = true;

            float _roadSpeed = inputController.GetRoadSpeed();
            inputController.SetRoadSpeed(_roadSpeed / 2);

            other.transform.GetChild(1).GetComponent<EnemyController>().EnemyAttack(transform);

            StartCoroutine(UpdateEnemyPlayerStickmans());
        }

        if (other.CompareTag(TagList.Finish))
        {
            secondCamera.gameObject.SetActive(true);
            //FinishLine = true;
            TowerFormation.Instance.CreateTower(transform.childCount - 1);
            transform.GetChild(0).gameObject.SetActive(false);  // Diactivate label of Counter
        }
    }

    private IEnumerator UpdateEnemyPlayerStickmans ()
    {
        enemyStickmansCount = enemy.transform.GetChild(1).childCount - 1;
        playerStickmansCount = transform.childCount - 1;

        while (enemyStickmansCount > 0 && playerStickmansCount > 0)
        {
            enemyStickmansCount--;
            playerStickmansCount--;

            enemy.transform.GetChild(1).GetComponent<EnemyController>().UpdateLabelText();
            UpdateCounterText();

            yield return null;
        }

        if (enemyStickmansCount == 0)
        {
            RotateForwardStickmans();
            //isAttack = false; // Call Disable Attack () ?!!
        }
    }







    public int GetStickmansCount()
    {
        return playerStickmansCount;
    }

    public bool GetIsAttackPlayer()
    {
        return isAttack;
    }

    public void SetMoveTheCamera(bool value)
    {
        moveTheCamera = value;
    }

    public bool GetMoveTheCamera()
    {
        return moveTheCamera;
    }


}
