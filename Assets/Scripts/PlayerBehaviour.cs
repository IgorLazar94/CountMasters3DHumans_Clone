using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject stickMan;
    [SerializeField] private CoinManager coinManager;
    private TextMeshPro counterLabelText;
    [HideInInspector] public Transform player;
    private int playerStickmansCount;
    private int enemyStickmansCount;
    private InputController inputController;

    [Range(0f, 1f)] [SerializeField] float distanceBetween;
    [Range(0f, 1f)] [SerializeField] float radius;

    private bool isAttack;
    private float distanceToAttack;
    private Transform enemy;
    private Transform enemyControllerObject;


    private void Start()
    {
        Instance = this;
        distanceToAttack = GameSettings.Instance.GetDistanceToAttack();

        inputController = gameObject.GetComponent<InputController>();
        player = transform;
        counterLabelText = GetComponentInChildren<TextMeshPro>();
        playerStickmansCount = transform.childCount - 1;
        EnableAnimation();
        UpdateCounterText();
    }

    private void EnableAnimation()
    {
        player.GetChild(1).GetComponent<Animator>().SetBool("isRunning", true);
    }

    private void UpdateCounterText ()
    {
        counterLabelText.text = playerStickmansCount.ToString();
    }
   
    private void FixedUpdate()
    {
        if (isAttack)
        {
            PlayerAttack();

            if (transform.childCount == 1) // if blue == 0 () => StopAttack
            {
                enemyControllerObject.gameObject.GetComponent<EnemyController>().StopAttacking();
                gameObject.SetActive(false);
            } 
        }
    }

    private void RotateOnEnemy()
    {
        var enemyDirection = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z) - transform.position;

        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation,
                                                              Quaternion.LookRotation(enemyDirection, Vector3.up),
                                                              Time.deltaTime * 3f);
        }
    }

    private void PlayerAttack()
    {
        RotateOnEnemy();

        if (enemyControllerObject.childCount > 1)
        {
            CheckDistance();
        }
        else
        {
            DisableAttack();
            enemy.gameObject.SetActive(false);
        }
    }

    private void CheckDistance()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var distance = enemyControllerObject.GetChild(0).position - transform.GetChild(i).position;

            if (distance.magnitude < distanceToAttack)
            {
                transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                                                        new Vector3(enemyControllerObject.GetChild(0).position.x,
                                                                    transform.GetChild(i).position.y,
                                                                    enemyControllerObject.GetChild(0).position.z), Time.deltaTime * 1f);

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
            var stickman = Instantiate(stickMan, new Vector3(transform.position.x, 
                                                             transform.position.y - 1, 
                                                             transform.position.z), Quaternion.identity, transform);
            stickman.GetComponent<StickmanBehaviour>().SetPlayerBehaviour(this);
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
            enemyControllerObject = enemy.transform.GetChild(1).gameObject.transform;
            isAttack = true;

            float _roadSpeed = inputController.GetRoadSpeed();
            inputController.SetRoadSpeed(_roadSpeed / 2);

            other.transform.GetChild(1).GetComponent<EnemyController>().EnemyAttack(transform);

            StartCoroutine(UpdateEnemyPlayerStickmans());
        }

        if (other.CompareTag(TagList.Finish))
        {
            cameraController.ActivateSecondCamera();
            //FinishLine = true;
            TowerFormation.Instance.CreateTower(transform.childCount - 1);
            DiactivateLabel();
        }
    }

    private void DiactivateLabel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private IEnumerator UpdateEnemyPlayerStickmans ()
    {
        enemyStickmansCount = enemyControllerObject.childCount - 1;
        playerStickmansCount = transform.childCount - 1;

        while (enemyStickmansCount > 0 && playerStickmansCount > 0)
        {
            enemyStickmansCount--;
            playerStickmansCount--;
            HandOverAddCoin();
            enemyControllerObject.gameObject.GetComponent<EnemyController>().UpdateLabelText();
            UpdateCounterText();

            yield return null;
        }

        if (enemyStickmansCount == 0)
        {
            RotateForwardStickmans();
        }
    }

    public void HandOverAddCoin()
    {
        coinManager.AddPlayerCoin();
    }

    public int HandOverGetCoin()
    {
        return coinManager.GetPlayerCoin();
    }







    public int GetStickmansCount()
    {
        return playerStickmansCount;
    }

    public bool GetIsAttackPlayer()
    {
        return isAttack;
    }

    //public void SetMoveTheCamera(bool value)
    //{
    //    cameraController.ActivateSecondCamera();
    //}

    //public bool GetMoveTheCamera()
    //{
    //    return moveTheCamera;
    //}


}
