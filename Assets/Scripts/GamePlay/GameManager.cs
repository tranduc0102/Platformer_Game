using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int livePlayer = 3;
    private int enemyCount;
    public GameObject loadNextLevel;
    public int idAnimator = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // UpdateEnemyCount();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // UpdateEnemyCount();
        // if (enemyCount == 0)
        // {
        //     loadNextLevel.SetActive(true);
        //     Invoke("loadNext",1.3f);
        // }
    }

    // private void UpdateEnemyCount()
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //     enemyCount = enemies.Length;
    // }
    //
    // private void loadNext()
    // {
    //     livePlayer = 3;
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }
}