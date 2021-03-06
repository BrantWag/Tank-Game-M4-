﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int numAIToSpawn;
    public int numAICurrent;

    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public Transform charactersHolder;
    public Transform pickupsHolder;

    public List<TankData> players;
    public List<TankData> aiUnits;

    public List<Transform> characterSpawns;
    public List<GameObject> spawnedItems;

    // Created temporarily
    public GameObject player1;

    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        respawnAI();
        if (player1 == null)
        {
            player1 = respawnPlayer();
        }
	}

    // Respawn AIs
    void respawnAI()
    {
        while (numAICurrent < numAIToSpawn)
        {
            int randomNum = Random.Range(0, characterSpawns.Count-1);
            Transform locationToSpawn = characterSpawns[randomNum];
            GameObject newAI = Instantiate(aiPrefab, locationToSpawn);
            newAI.transform.SetParent(charactersHolder);
            setAiWaypoints(newAI, locationToSpawn);
            numAICurrent++;
        }
    }

    // Set waypoints for new AI
    void setAiWaypoints(GameObject aiSpawned, Transform spawnLocation)
    {
        foreach (Transform point in spawnLocation.gameObject.GetComponentInParent<Room>().waypoints)
        {
            aiSpawned.GetComponent<Controller_AI>().waypoints.Add(point);
        }
    }

    // Respawn players
    GameObject respawnPlayer()
    {
        int randomNum = Random.Range(0, characterSpawns.Count-1);
        Transform spawnLocation = characterSpawns[randomNum];
        GameObject player = Instantiate(playerPrefab, spawnLocation);
        player.transform.SetParent(charactersHolder);
        return player;
    }
}
