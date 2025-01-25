using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField]
    public int damageValue;

    public GameObject ref_ScoreManager;

    public Rigidbody body;

    [SerializeField]
    public ParticleSystem damageParticles;
    private int playerScore;
    [SerializeField]

    public GameObject floatingScoreText;

    Renderer m_renderer;
    public Color attackColor = Color.red;
    public Color decayColor = Color.gray;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();

        ref_ScoreManager = GameObject.Find("ScoreManager");

    }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        damageParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // Damage player
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageValue);

            // set speed to 0
            this.gameObject.GetComponent<EnemyMovement>().enemySpeed = 0f;

            m_renderer.material.color = attackColor;
            //Play Particles
            damageParticles.Play();

            //Play Audio

            //Destroy Self
            Invoke("DestroySelf", 0.25f);

        }


        if (collision.gameObject.tag == "PlayerSaber")
        {
            //Set Speed to 0
            this.gameObject.GetComponent<EnemyMovement>().enemySpeed = 0f;
            m_renderer.material.color = decayColor;

            //update score
            ref_ScoreManager.GetComponent<ScoreManager>().AddScore(1);
            playerScore = ref_ScoreManager.GetComponent<ScoreManager>().GetScore();

            ShowFloatingText();

            // Play Audio


            Invoke("DestroySelf", 0.25f);

        }
    }

    public void DestroySelf()
    {
        //Play Particles here

        DestroyImmediate(this.gameObject);

    }

    void ShowFloatingText()
    {
        Vector3 textPosition = transform.position + Vector3.up * .5f;  // Position above enemy

        GameObject floatingText = Instantiate(floatingScoreText, textPosition, Quaternion.identity);

        FloatingText textComponent = floatingText.GetComponentInChildren<FloatingText>();
        if (textComponent != null)
        {
           // textComponent.SetText(playerScore.ToString());
        }
    }
}
