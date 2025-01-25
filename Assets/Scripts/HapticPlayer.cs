using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticPlayer : MonoBehaviour
{
    // this component is for controlling the vibration on each haptic location
    // 0 for linear, 1 for damping sine wave, 2 for double peaks
    [SerializeField] HapticClipCollections hapticClipCollections;
    public float intensity=0f;
    // deltaTime = time passed after the vibration is triggered
    float deltaTime = 0;
    public bool isVibrating=false;
    HapticClip activeHapticClip;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        activeHapticClip = hapticClipCollections.hapticClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // constant Vibrating
        if (isVibrating)
        {
            deltaTime += Time.deltaTime;
        }
        CheckVibration();
    }

    void CheckVibration()
    {
        if (isVibrating)
        {
            if(deltaTime > activeHapticClip.duration)
            {
                isVibrating = false;
            }
            intensity = FindIntensityInPattern(activeHapticClip);

        }
    }

    float FindIntensityInPattern(HapticClip hapticClip)
    {
        if (hapticClip!=null)
        {
            return hapticClip.hapticPattern.Evaluate(deltaTime/hapticClip.duration);
        }
        return 0;
    }

    public void SetActiveHapticClip(int index)
    {
        if (index < hapticClipCollections.hapticClips.Length)
        {
            activeHapticClip =hapticClipCollections.hapticClips[index];
        }
        isVibrating = true;
    }
}
