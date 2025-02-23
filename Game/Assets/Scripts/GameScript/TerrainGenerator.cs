﻿using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainDatas = new();
    [SerializeField] private Transform terrainHolder;

    private readonly List<GameObject> currentTerrains = new();
    private Vector3 currentPosition = new(0, 0, 0);


    private void Start()
    {
        for (var i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrain(true, new Vector3(0, 0, 0));
        }

        maxTerrainCount = currentTerrains.Count;
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {
        if (currentTerrains.Count < 5)
        {
            if (currentPosition.x - playerPos.x < minDistanceFromPlayer || isStart)
            {
                var whichTerrain = 0;
                var terrainVariation = Random.Range(0, terrainDatas[whichTerrain].possibleTerrain.Count);

                var terrain = Instantiate(
                    terrainDatas[whichTerrain].possibleTerrain[terrainVariation],
                    currentPosition, Quaternion.identity, terrainHolder
                );

                currentTerrains.Add(terrain);
                currentPosition.x++;
            }

            return;
        }

        if (currentPosition.x - playerPos.x < minDistanceFromPlayer || isStart)
        {
            var whichTerrain = 0;
            if (LevelSelector.LevelGame() < 2)
            {
                do
                {
                    whichTerrain = Random.Range(0, terrainDatas.Count);
                } while (terrainDatas[whichTerrain].isRail);
            }
            else
            {
                whichTerrain = Random.Range(0, terrainDatas.Count);
            }

            var terrainInSuccession = Random.Range(1, terrainDatas[whichTerrain].maxInSuccession);
            for (var i = 0; i < terrainInSuccession; i++)
            {
                var terrainVariation = Random.Range(0, terrainDatas[whichTerrain].possibleTerrain.Count);
                if (terrainDatas[whichTerrain].isWater)
                {
                    terrainVariation = ((int)currentPosition.x) % 3;
                }

                var terrain = Instantiate(
                    terrainDatas[whichTerrain].possibleTerrain[terrainVariation],
                    currentPosition, Quaternion.identity, terrainHolder
                );


                if (!isStart)
                {
                    if (currentTerrains.Count > maxTerrainCount)
                    {
                        Destroy(currentTerrains[0]);
                        currentTerrains.RemoveAt(0);
                    }
                }

                currentTerrains.Add(terrain);
                currentPosition.x++;
            }
        }
    }
}