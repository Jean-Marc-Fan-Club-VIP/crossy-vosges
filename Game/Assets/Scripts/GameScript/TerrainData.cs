﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data", menuName = "Terrain Data")]
public class TerrainData : ScriptableObject
{
    public List<GameObject> possibleTerrain;
    public bool isWater;
    public bool isRail;
    public int maxInSuccession;
}