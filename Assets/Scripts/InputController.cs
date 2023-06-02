using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Transform road;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool gameState;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float roadSpeed;

    private PlayerBehaviour playerBehaviour;
    private Vector3 mouseStartPos, playerStartPos;
    private Camera mainCamera;

    private void Start()
    {
        playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!playerBehaviour.GetIsAttackPlayer())
        {
            PlayerMove();
        }


        if (gameState)
        {
            MoveRoad();
            EnableAnimation();
        }
    }

    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            isMoving = true;

            var plane = new Plane(Vector3.up, 0f);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1f);
                playerStartPos = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if (plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);
                var move = mousePos - mouseStartPos;
                Vector3 offset = playerStartPos + move;

                CheckBorders(offset);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, offset.x, Time.deltaTime * playerSpeed),
                                                            transform.position.y,
                                                            transform.position.z);
            }
        }
    }

    private void CheckBorders(Vector3 _offset)
    {
        if (playerBehaviour.GetStickmansCount() >= 50)  // разбить на большее кол-во счёта (границы игрока)
        {
            _offset.x = Mathf.Clamp(_offset.x, -2.2f, 2.2f);
        }
        else
        {
            _offset.x = Mathf.Clamp(_offset.x, -4f, 4f);
        }
    }

    private void EnableAnimation()
    {
        for (int i = 1; i < transform.childCount; i++) // enable animation
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("isRunning", true);
        }
    }

    private void MoveRoad()
    {
        road.Translate(road.forward * Time.deltaTime * roadSpeed * (-1)/*reverse*/);  // move road
    }

    public void SetRoadSpeed(float value)
    {
        roadSpeed = value;
    }

    public float GetRoadSpeed()
    {
        return roadSpeed;
    }
}
