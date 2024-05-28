using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D area;
    public float minDistanceFromSnake = 1.0f;
    // Metoda wywo³ywana przy starcie
    void Start()
    {
        MoveFood();
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
            other.gameObject.GetComponent<Snake>().Grow();
        }
    }
    void MoveFood()
    {
        Bounds bounds = this.area.bounds;
        Vector3 newPosition;

        // Pêtla do znajdowania odpowiedniej pozycji
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            newPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
        }
        while (IsPositionCloseToSnake(newPosition));

        transform.position = newPosition;
    }

    bool IsPositionCloseToSnake(Vector3 position)
    {
        GameObject snake = GameObject.FindWithTag("Player");
        foreach (Transform segment in snake.GetComponent<Snake>().tailSegments)
        {
            if (Vector3.Distance(position, segment.position) < minDistanceFromSnake)
            {
                return true;  // Pozycja jest zbyt blisko wê¿a
            }
        }
        return false;  // Pozycja jest odpowiednia
    }
}


