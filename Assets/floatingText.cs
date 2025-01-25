using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class floatingText : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float lifetime = 3f;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Destroy(gameObject, lifetime);
        text.alpha = 0;
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        text.alpha -= Time.deltaTime / lifetime; // Gradually fade out
    }

    public void SetText(string scoreValue)
    {
        text.text = scoreValue;
        text.alpha = 1;
    }
}
