using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    public ParticleSystem exposionParticle;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();

        targetRb.AddForce(randomForce(), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(), randomTorque(), randomTorque(),  ForceMode.Impulse);

        transform.position = randomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
    
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(exposionParticle, transform.position, exposionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //the ide really didn't like gameManager.GameOver(); so I did a workaround

        if (!gameObject.CompareTag("bad"))
        {
            gameManager.gameOverText.gameObject.SetActive(true);
            //i'm still doing a workaround
            gameManager.isGameActive = false;
            //still workaround
            gameManager.restartButton.gameObject.SetActive(true);
        }
    }

    Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float randomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 randomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
