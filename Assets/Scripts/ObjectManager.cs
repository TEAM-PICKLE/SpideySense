using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //AKA EnemyManager
    //Add this script to Manager gameobject in main, and have spawner manager and spheres to find the manager gameobject and the class on Awake
    public List<Transform> enemies;
    
    public void AddEnemy(Transform enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Transform enemy) {
        enemies.Remove(enemy);
    }
}
