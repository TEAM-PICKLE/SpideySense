using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float lifetime = 3f;
    private TextMeshProUGUI m_text;
    private Transform vrCamera;

    void Start()
    {
        m_text = GetComponentInChildren<TextMeshProUGUI>();
        m_text.text = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score.ToString();
        vrCamera = Camera.main.transform;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    void Update()
    {
        // Move the text upwards gradually
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Make text face the VR player
        transform.LookAt(transform.position + (transform.position - vrCamera.position));
    }

    //public void SetText(string scoreValue) => m_text.text = scoreValue;
}
