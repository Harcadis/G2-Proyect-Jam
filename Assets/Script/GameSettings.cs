﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "MoleInvaders/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public int maxPowerUps;
    public float moveCooldown;

    public int initialPowerUpsOnGrid;
    public float powerUpsSpawnRate;
    public int obstaclesNumber;

    public int superDrillsAtStart;
    public int superDrillsPerPowerUp;


    public bool unlimitedTime;
    public float gameDuration;
}