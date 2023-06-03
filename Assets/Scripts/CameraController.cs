using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera secondCamera;
    [SerializeField] private Transform playerBehaviour;
    private CinemachineTransposer cinemachineTransposer;
    private CinemachineComposer cinemachineComposer;
    private bool moveTheCamera = false;
    private float secondCameraOffset = 6f;



    private void LateUpdate()
    {
        if (moveTheCamera && playerBehaviour.childCount > 1)
        {
            SecondCameraMove();
        }
    }


    private void SecondCameraMove()
    {


        cinemachineTransposer.m_FollowOffset = new Vector3(secondCameraOffset,
                                                           Mathf.Lerp(cinemachineTransposer.m_FollowOffset.y,
                                                                      playerBehaviour.GetChild(1).position.y + secondCameraOffset,
                                                                      Time.deltaTime * 1f),
                                                           -secondCameraOffset);

        cinemachineComposer.m_TrackedObjectOffset = new Vector3(0f,
                                                                Mathf.Lerp(cinemachineComposer.m_TrackedObjectOffset.y,
                                                                          secondCameraOffset,
                                                                          Time.deltaTime * 1f),
                                                               0f);
    }
    public void ActivateSecondCamera()
    {
        secondCamera.gameObject.SetActive(true);

        cinemachineTransposer = secondCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        cinemachineComposer = secondCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();

        moveTheCamera = true;

    }

    public bool GetIsMoveSecondCamera()
    {
        return moveTheCamera;
    }

   
}
