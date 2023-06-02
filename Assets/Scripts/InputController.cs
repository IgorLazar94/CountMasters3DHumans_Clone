using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Transform road;
    [SerializeField] private bool moveByTouch;
    [SerializeField] private bool gameState;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float roadSpeed;

    private Vector3 mouseStartPos, playerStartPos;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            moveByTouch = true;

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
            moveByTouch = false;
        }

        if (moveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if (plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);
                var move = mousePos - mouseStartPos;

                var control = playerStartPos + move;
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed), 
                                                            transform.position.y, 
                                                            transform.position.z);
            }
        }

        if (gameState)
        {
            road.Translate(road.forward * Time.deltaTime * roadSpeed * (-1)/*reverse*/);
        }
    }
}
