using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    [Space]
    [Header("Stickmans")]
    [Range(5f, 10f)] [SerializeField] private float distanceToAttack;
    [Range(1f, 3f)] [SerializeField] private float jumpForce;
    [Range(1f, 3f)] [SerializeField] private float jumpDuration;
    [Space]
    [Header("Track")]
    [SerializeField] private float trackSpeed;



    // Stickmans
    public float GetDistanceToAttack()
    {
        return distanceToAttack;
    }
    public float GetJumpDuration()
    {
        return jumpDuration;
    }
    public float GetJumpForce()
    {
        return jumpForce;
    }

    // Track
    public float GetTrackSpeed()
    {
        return trackSpeed;
    }

}
