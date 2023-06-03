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



    private void LateUpdate()
    {
        if (moveTheCamera && playerBehaviour.childCount > 1)
        {
            SecondCameraActivated();
        }
    }


    private void SecondCameraActivated()
    {


        cinemachineTransposer.m_FollowOffset = new Vector3(4.5f,
                                                           Mathf.Lerp(cinemachineTransposer.m_FollowOffset.y,
                                                                      playerBehaviour.GetChild(1).position.y + 2f,
                                                                      Time.deltaTime * 1f),
        -5f);

        cinemachineComposer.m_TrackedObjectOffset = new Vector3(0f,
                                                                Mathf.Lerp(cinemachineComposer.m_TrackedObjectOffset.y,
                                                                          4f,
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

    public bool IsMoveSecondCamera()
    {
        return moveTheCamera;
    }

   
}
