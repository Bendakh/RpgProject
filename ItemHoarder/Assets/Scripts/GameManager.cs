using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private bool isFighting;

    public bool isLoaded = false;
    public PlayerSaveData mockData;
    public bool IsFighting { get => isFighting; set => isFighting = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        //DetectPlayer();
    }

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {                 
        SceneManager.sceneLoaded -= OnSceneLoaded;       
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        if(scene.buildIndex == 1)
        {
            DetectPlayer();
        }
    }

    public void DetectPlayer()
    {
        if (player == null && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>())
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    public void StartCombat()
    {
        isFighting = true;
    }

    public void EndCombat()
    {
        isFighting = false;
    }

    public void InitializePlayerData(PlayerSaveData playerSaveData)
    {
        _instance.player.InitializePlayerData(playerSaveData);
    }
}
