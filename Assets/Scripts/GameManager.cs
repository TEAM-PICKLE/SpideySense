using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    HapticPlayer haptic;
   
    // Start is called before the first frame update
    void Start()
    {
        haptic = GetComponentInParent<HapticPlayer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (haptic != null) { if (haptic.isVibrating) this.gameObject.SetActive(true); else this.gameObject.SetActive(false); }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
