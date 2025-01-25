using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HapticClipCollections", menuName = "ScriptableObjects/Haptic Clip Collections", order = 1)]
public class HapticClipCollections : ScriptableObject
{
    public HapticClip[] hapticClips;
}
