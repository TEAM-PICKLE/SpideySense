using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HapticClip", menuName = "ScriptableObjects/Haptic Clip", order = 1)]
public class HapticClip : ScriptableObject
{
    public AnimationCurve hapticPattern;
    public int duration;
}
