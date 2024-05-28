using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    public List<Transform> tailSegments = new List<Transform>();
    public Transform snakeBodyPart;
    public TMP_Text gameState;
    bool gameOn = true;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        tailSegments.Add(this.transform);
    }
    // Update is called once per frame (every 1s)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {
        for (int i = tailSegments.Count - 1; i > 0; i--)
        {
            tailSegments[i].position = tailSegments[i - 1].position;
        }
        Vector3 movement = new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);

        transform.position += movement;



        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );

        for (int i = 1; i < tailSegments.Count; i++)
        {
            if (this.transform.position == tailSegments[i].position)
            {
                Debug.Log("The snake is trying to eat itself!");
                GameOver();
            }
        }
        if (gameOn)
        gameState.text = "Score: " + (tailSegments.Count-1).ToString();
    }
    public void Grow()
    {
        Transform segment = Instantiate(this.snakeBodyPart);
        segment.position = tailSegments[tailSegments.Count - 1].position;
        tailSegments.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Board"))
        {
            Debug.Log("Board collision!");
            GameOver();
        }
    }
    private void GameOver()
    {
        gameState.text = "Game Over";
        for (int i = 1; i < tailSegments.Count; i++)
        {
            Destroy(tailSegments[i].gameObject);
        }
        tailSegments.Clear();
        tailSegments.Add(this.transform);
        this.transform.position = Vector3.zero;
        gameOn = false;
        Invoke("SetGameOverText", 1f);
    }
    private void SetGameOverText()
    {
        gameOn = true;
    }
}