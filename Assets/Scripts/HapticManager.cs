using Sngty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HapticManager : MonoBehaviour
{
    public List<HapticPlayer> hapticPlayers = new List<HapticPlayer>();
    [SerializeField] SingularityManager singularityManager;
    float lowestDistance;
    bool isDestorying = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SendAll", 1f, 0.05f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // edit here to stop vibrating every second
        //SendAll();
    }

    HapticPlayer FindCloestHapticPlayer(Transform enemy)
    {
        int j = 0; // lowest index
        lowestDistance = Vector3.Distance(enemy.position, hapticPlayers[0].transform.position);
        for (int i =0; i < hapticPlayers.Count;i++){
            if(Vector3.Distance(enemy.position, hapticPlayers[i].transform.position) < lowestDistance)
            {
                lowestDistance = Vector3.Distance(enemy.position, hapticPlayers[i].transform.position);
                j = i;
            }
            else
            {
                hapticPlayers[i].isVibrating = false;
            }
        }

        return hapticPlayers[j];
    }

    public void TriggerCloestHapticPlayer(Transform enemy)
    {
        HapticPlayer closestPlayer = FindCloestHapticPlayer(enemy);
        closestPlayer.distance = lowestDistance;
        if (!closestPlayer.isVibrating)
        {
            closestPlayer.isVibrating = true;
        }
    }
    void SendAll()
    {
        if (!isDestorying)
        {
            for (int i = 0; i < hapticPlayers.Count; i++)
            {
                string message = "";
                if (hapticPlayers[i] != null)
                {
                    if (hapticPlayers[i].isVibrating)
                    {
                        message = $"{(i + 1)}-{Mathf.FloorToInt(hapticPlayers[i].intensity * 100)}";
                    }
                    else
                    {
                        message = $"{(i + 1)}-{0}";
                    }
                    singularityManager.sendMessage(message);
                    if (hapticPlayers[i].intensity != 1 && hapticPlayers[i].intensity != 0)
                    {
                        print(message);
                    }
                }
            }
        }

    }

    private void OnDestroy()
    {
        isDestorying = true;
        for (int i = 0;i < 6; i++)
        {
            singularityManager.sendMessage($"{i+1}-{0}");
        }


    }
}
