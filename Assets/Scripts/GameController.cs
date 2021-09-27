using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] Health _bossHealth;
    public static bool _playerIsAlive;
    public static bool _bossIsAlive = true;

    private void OnEnable()
    {
        _playerHealth.Died += OnPlayerDied;
        _bossHealth.Died += OnBossDied;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void OnDisable()
    {
        _playerHealth.Died -= OnPlayerDied;
        _bossHealth.Died -= OnBossDied;
    }

    private void OnPlayerDied()
    {
        _playerIsAlive = false;
    }

    private void OnBossDied()
    {
        _bossIsAlive = false;
    }


}
