using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnRockSpawnPoints;
    [SerializeField] private GameObject rockGameObject;
    [SerializeField] private Transform shipTransform;
    [SerializeField] private PointSystem pointSystem;
    [SerializeField] private Stats playerStats;

    [SerializeField] private Slider rockCountSlider;
    [SerializeField] private TMP_Text spawnRockCountText;

    // My Timers for Spawning Rocks
    [SerializeField] private float spawnRockTimer = 0f;
    [SerializeField] private float defaultRockSpawnTimer = 3f;

    [SerializeField] private int spawnRockCount = 3;    

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this game object
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this
        Instance = this;

        // Optional: Keep this object alive between scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartingRocksSpawn());
        spawnRockTimer = defaultRockSpawnTimer;
    }

    private IEnumerator StartingRocksSpawn() // starts spawning the rocks in the beginning.
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Starting Rocks Spawned!");
        SpawnRock();
    }

    // Update is called once per frame
    void Update()
    {
        spawnRockTimer -= Time.deltaTime;

        if (spawnRockTimer < 0)
        {
            SpawnRock();
            spawnRockTimer = defaultRockSpawnTimer;
        }

        spawnRockCount = (int)rockCountSlider.value; 
        spawnRockCountText.text = $"Rocks: {spawnRockCount}";

        PauseGameOnWindowsKey(true);
    }

    private void PauseGameOnWindowsKey(bool isActive) // this is temp; just an idea
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftWindows))
            {
                Debug.Log("Paused Game");
            }
            if (Input.GetKeyDown(KeyCode.RightWindows))
            {
                Debug.Log("Paused Game");
            }
        }
    }

    private void SpawnRock()
    {
        for (int i = 0; i < spawnRockSpawnPoints.Length; i++)
        {
            for (int j = 0; j < spawnRockCount; j++)
            {
                GameObject spawnedRock = Instantiate(rockGameObject, spawnRockSpawnPoints[i].position, spawnRockSpawnPoints[i].rotation);
                spawnedRock.GetComponent<Rock>().SetShipTarget(shipTransform);
                spawnedRock.GetComponent<Rock>().SetPointSystem(pointSystem);
                spawnedRock.GetComponent<Rock>().SetStats(playerStats);
            }
        }
    }

    public void GameOver()
    {

    }
}

