using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    // Metoda wywo³ywana przy starcie
    void Start()
    {
    }

    // Metoda wywo³ywana co klatkê
    void Update()
    {
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Kolizja z wê¿em!");
            MoveFood();

            //other.GetComponent<Snake>().Grow();
        }
    }
    void MoveFood()
    {
        // Generowanie nowej losowej pozycji w okreœlonym zakresie
        float x = Random.Range(-8.0f, 8.0f);
        float y = Random.Range(-4.0f, 4.0f);
        transform.position = new Vector2(x, y);
    }
}

