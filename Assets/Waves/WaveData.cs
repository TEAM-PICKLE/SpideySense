using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class WaveData : ScriptableObject
{
    public bool[] activeSpawners = new bool[8];
    public float[] enemySpeed = new float[8];
}
