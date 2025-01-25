using Sngty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
    public List<HapticPlayer> hapticPlayers = new List<HapticPlayer>();
    [SerializeField] SingularityManager singularityManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // edit here to stop vibrating every second
        SendAll();
    }

    HapticPlayer FindCloestHapticPlayer(Transform enemy)
    {
        HapticPlayer closestPlayer = hapticPlayers[0];
        float[] distances = new float[hapticPlayers.Count];
        for(int i =0; i < hapticPlayers.Count;i++){
            distances[i] = Vector3.Distance(enemy.position, hapticPlayers[i].transform.position);
        }
        return closestPlayer;
    }

    public void TriggerCloestHapticPlayer(Transform enemy, int patternID)
    {
        HapticPlayer closestPlayer = FindCloestHapticPlayer(enemy);
        if (!closestPlayer.isVibrating)
        {
            closestPlayer.SetActiveHapticClip(patternID);
        }
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
