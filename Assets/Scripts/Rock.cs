using System.Collections;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Rock : MonoBehaviour
{
    [SerializeField] private Transform currentShipTransform;

    [SerializeField] private Rigidbody2D rockRigidBody;

    [SerializeField] private float rockSpeed = 2f;

    [SerializeField] private float currentRockDamage = 2f;
    [SerializeField] public float rockDamage = 2f;
    [SerializeField] public float currentRockHealth = 100f;
    [SerializeField] public float maxRockHealth = 100f;
    [SerializeField] public float rewardPoints = 0f;

    [SerializeField] private bool isDestroyed = false;

    [SerializeField] private RockVisual rockVisual;
    private PointSystem pointSystem;
    private Stats playerStats;

    private const string SHIP_TAG = "Ship";


    private void Awake()
    {
        rockSpeed = Random.Range(0.2f, 0.4f);

        rockSpeedMultiplier(Random.Range(1.2f, 1.4f));
        currentRockDamage = Random.Range(2, 25);
        rockDamage = currentRockDamage; // updating rockDamage; using it as an API for other scripts
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = currentShipTransform.position; // Setting our target to the ship
        Vector2 newPosition = Vector2.MoveTowards(rockRigidBody.position, targetPosition, rockSpeed * Time.fixedDeltaTime); // move towards the ship target
        rockRigidBody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(SHIP_TAG))
        {
            Ship ship = collision.GetComponent<Ship>();
            ship.TakeShipDamage(currentRockDamage); // take damage on the ship

            Destroy(gameObject);
        }
    }

    private void rockSpeedMultiplier(float mult) // future me: if rock bigger = slower
    {
        rockSpeed *= mult;
        //Debug.Log(rockSpeed);
    }

    public void SetShipTarget(Transform shipTransform)  
    {
        currentShipTransform = shipTransform; // find ship through this function since its being spawned. Whatever I set the ship to on the GameManager, it will set it here.
    }

    public void SetPointSystem(PointSystem PointSystem)
    {
        pointSystem = PointSystem;
    }
    public void SetStats(Stats PlayerStats)
    {
        playerStats = PlayerStats;
    }

    private void OnMouseDown() 
    {
        if(!isDestroyed)
        {
            if (currentRockHealth > 0)
            {
                currentRockHealth -= playerStats.maxClickDamage;
            }
            if (currentRockHealth <= 0)
            {
                isDestroyed = true;
                StartCoroutine(DelayDestroy(0.5f));
            }
        }
    }

    private IEnumerator DelayDestroy(float timer)
    {
        rockVisual.PlayRewardAnimation();
        Debug.Log("Delay Destoryed Started!");
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        pointSystem.AddPoint(rewardPoints);
    }
}
