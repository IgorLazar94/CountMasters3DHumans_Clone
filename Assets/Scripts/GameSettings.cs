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


    // Stickmans
    public float GetDistanceToAttack()
    {
        return distanceToAttack;
    }
    
}
