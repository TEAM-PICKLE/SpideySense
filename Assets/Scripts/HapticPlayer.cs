using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticPlayer : MonoBehaviour
{
    // this component is for controlling the vibration on each haptic location
    public float intensity=0f;
    public bool isVibrating=false;
    HapticClip activeHapticClip;
    public float range=5;
    public float distance=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        CheckVibration();
    }

    void CheckVibration()
    {
        if (isVibrating)
        {
            intensity = Mathf.Clamp((range-distance),0,range)/range;
        }
    }
}
