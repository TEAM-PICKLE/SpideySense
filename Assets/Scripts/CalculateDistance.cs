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
        
    }

    void CalcuateDistanceForEachHapticPlayer()
    {
        foreach (Transform enemy in objectManager.enemies)
        {
            hapticManager.TriggerCloestHapticPlayer(enemy,0);
        }
    }
}
