using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _setting;
    [SerializeField] private Button _rePlay;
    [SerializeField] private GameObject objSetting;

    private void Start()
    {
        _setting.onClick.AddListener(()=>Setting());
        _rePlay.onClick.AddListener(()=>Replay());
    }

    private void Setting()
    {
        objSetting.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Replay()
    {
        GameManager.Instance.livePlayer = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
