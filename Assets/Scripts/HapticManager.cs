using Sngty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
    [SerializeField] List<HapticPlayer> hapticPlayers = new List<HapticPlayer>();
    [SerializeField] SingularityManager singularityManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SendAll();
    }
    void SendAll()
    {
        for (int i = 0; i < hapticPlayers.Count; i++)
        {
            string message = $"{i}-{Mathf.FloorToInt(hapticPlayers[i].intensity * 100)}";
            singularityManager.sendMessage(message);
        }
    }
}
