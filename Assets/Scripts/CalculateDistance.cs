using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[RequireComponent(typeof(ObjectManager))]
public class CalculateDistance : MonoBehaviour
{
    ObjectManager objectManager;
    [SerializeField] HapticManager hapticManager;
    private void Awake()
    {
        objectManager = GetComponent<ObjectManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectManager.enemies.Count > 0) { CalcuateDistanceForEachHapticPlayer(); }
    }

    void CalcuateDistanceForEachHapticPlayer()
    {
        foreach (Transform enemy in objectManager.enemies)
        {
            if (enemy != null)
            {
                hapticManager.TriggerCloestHapticPlayer(enemy);
            }
        }
    }
}
