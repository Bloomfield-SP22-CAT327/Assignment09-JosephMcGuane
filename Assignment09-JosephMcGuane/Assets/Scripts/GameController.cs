using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class GameController : MonoBehaviour
{
    public static GameController current;
    public GameObject player;
    public GameObject respawnZone;
    public Text hudTime;
    public Text hudPosition;

    private float playerX;
    private float playerY;
    private float playerZ;

    private bool isRunning = false;

    private float time = 0;
    private float position = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        playerZ = player.transform.position.z;

        if (isRunning)
        {
            time += Time.deltaTime;
            hudTime.text = "Your time is " + (int)time;

            position = player.transform.position.x + player.transform.position.z;
            hudPosition.text = "Your current position is " + (int)position + " units from spawn.";
        }

        else
        {
            hudTime.text = "Your time was " + (int)time;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerSave();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerReload();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerDelete();
        }
    }

    public void PlayerSave()
    {
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.SetFloat("postion", position);
        PlayerPrefs.SetFloat("playerX", playerX);
        PlayerPrefs.SetFloat("playerY", playerY);
        PlayerPrefs.SetFloat("playerZ", playerZ);
        PlayerPrefs.Save();
        SaveLoad.Save();
    }

    public void PlayerReload()
    {
        time = PlayerPrefs.GetFloat("time");
        position = PlayerPrefs.GetFloat("position");
        playerX = PlayerPrefs.GetFloat("playerX");
        playerY = PlayerPrefs.GetFloat("playerY");
        playerZ = PlayerPrefs.GetFloat("playerZ");
        player.transform.position = new Vector3(playerX, playerY, playerZ);
        SaveLoad.Load();
    }

    public void PlayerDelete()
    {
        PlayerPrefs.DeleteKey("time");
        PlayerPrefs.DeleteKey("position");
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("playerX");
    }

    public void RespawnPlayer()
    {
        player.gameObject.SetActive(false);
        player.gameObject.transform.position = respawnZone.transform.position;
        player.gameObject.SetActive(true);
        PlayerPrefs.DeleteKey("time");
        PlayerPrefs.DeleteKey("position");
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("playerX");
    }

    public void InitializeGame()
    {
        time = 0;
        position = 0;
        isRunning = true;
        RespawnPlayer();
    }

    public void Finish()
    {
        isRunning = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
