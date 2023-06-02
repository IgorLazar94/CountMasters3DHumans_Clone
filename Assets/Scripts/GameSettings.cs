using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    [Header("For Enemy Units")]
    [SerializeField] private float _timeEnemyWarriorSpown;
    [SerializeField] private int _level;
    [SerializeField] private int _koeficient;
    [SerializeField] private int _enemyEnergy;



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
    [Header("Player Catapult")]
    [SerializeField] private float playerProjectileSpeed = 60f;
    [SerializeField] private float playerReloadTime = 2.0f;
    [SerializeField] private float maxDistancePushForce = 60.0f;
    [Space]
    [Header("Enemy Catapult")]
    [SerializeField] private float enemyProjectileSpeed = 60f;  //?
    [SerializeField] private float enemyReloadTime = 4.0f; //?
    [SerializeField] private float enemyMissTarget_X = 5f;
    [Space]
    [Header("Damage From Projectiles")]
    [SerializeField] private int ballDamage = 10;
    [SerializeField] private int fireBallDamage = 25;
    [SerializeField] private int splitShotDamage = 5;
    [Range(0.5f, 2f)][SerializeField] private float splitShotTimeDivision = 1.0f;
    [Space]
    [Header("Player Trajectory")]
    [Range(20, 80)][SerializeField] private int dotsNumber = 60;
    [Range(0.05f, 0.2f)][SerializeField] private float dotSpacing = 0.15f;
    [Range(1f, 5f)][SerializeField] private float dotMinScale = 1f;
    [Range(5f, 15f)][SerializeField] private float dotMaxScale = 6f;
    [Space]
    [Header("Enemy Trajectory")]
    [Range(70f, 150f)][SerializeField] private float heightTrajectory = 70f;
    [Range(0f, 15f)][SerializeField] private float randomHeightCoefficient = 4f;
    [Space]
    [Header("List Of Targets For Enemy")]
    [Tooltip("В список перетащить объекты TargetForEnemy, которые лежат в объектах PlayerTower, PlayerCastle")]
    [SerializeField] private List<Transform> listOfTargets = new List<Transform>();

    // Player Catapult
    public float GetPlayerProjectileSpeed()
    {
        return playerProjectileSpeed;
    }
    public float GetPlayerReloadTime()
    {
        return playerReloadTime;
    }

    public float GetMaxDistancePushForce()
    {
        return maxDistancePushForce;
    }

    // Enemy Catapult
    public float GetEnemyProjectileSpeed()
    {
        return enemyProjectileSpeed;
    }
    public float GetEnemyReloadTime()
    {
        return enemyReloadTime;
    }
    public float GetEnemyMissTarget_X()
    {
        return enemyMissTarget_X;
    }

    //Damage From Projectiles
    public int GetBallDamage()
    {
        return ballDamage;
    }
    public int GetFireBallDamage()
    {
        return fireBallDamage;
    }
    public int GetSplitShotDamage()
    {
        return splitShotDamage;
    }
    public float GetSplitShotTimeDivision()
    {
        return splitShotTimeDivision;
    }

    // Player Trajectory
    public int GetDotsNumber()
    {
        return dotsNumber;
    }
    public float GetDotSpacing()
    {
        return dotSpacing;
    }
    public float GetDotMinScale()
    {
        return dotMinScale;
    }
    public float GetDotMaxScale()
    {
        return dotMaxScale;
    }
    // Enemy Trajectory
    public float GetHeightTrajectory()
    {
        return heightTrajectory;
    }

    public float GetRandomHeight()
    {
        return randomHeightCoefficient;
    }
    // List Of Targets For Enemy
    public List<Transform> GetListOfTargets()
    {
        return listOfTargets;
    }

    // For Enemy units spown
    public float TimeEnemyWarriorSpown
    {
        get { return _timeEnemyWarriorSpown; }
        set
        {
            _timeEnemyWarriorSpown = value;
        }
    }
    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
        }
    }
    public int Koeficient
    {
        get { return _koeficient; }
        set
        {
            _koeficient = value;
        }
    }
    public int EnemyEnergy
    {
        get { return _enemyEnergy; }
        set
        {
            _enemyEnergy = value;
        }
    }
}
