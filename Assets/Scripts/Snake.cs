using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//BIG NOTE: I can't change the transform of a gameobject in runtime if it's prefab.

public class Snake : MonoBehaviour
{

    public GameObject tailPrefab;
    public GameObject food;
    public GameObject canvas;
    public TextMeshPro scoreText;
    public TextMeshPro gameOverText;
    public bool isGameOver;

    public float speed = 1;
    

    private Vector2 areaLimit = new Vector2(13,24);
    private Vector2 direction = Vector2.down;
    private int score;
    private bool grow;
    


    private List<Transform> snake = new List<Transform>();

    
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        isGameOver = false;
        gameOverText.enabled = false;
        scoreText.text = "0";
        score = 0;
        MakeFoodInRandomPosition();
        StartCoroutine(Move());
        snake.Add(transform);

    }

    // Update is called once per frame
    void Update()
    {
        var rotationVector = gameObject.transform.rotation.eulerAngles;

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (direction != Vector2.right)){
            direction = Vector2.left;
            if (!isGameOver)
            {
                rotationVector.z = 270;
                gameObject.transform.rotation = Quaternion.Euler(rotationVector);
            }
            
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (direction != Vector2.left))
        {
            direction = Vector2.right;
            if (!isGameOver)
            {
                rotationVector.z = 90;
                gameObject.transform.rotation = Quaternion.Euler(rotationVector);
            }
            
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && (direction != Vector2.down))
        {
            direction = Vector2.up;
            if (!isGameOver)
            {
                rotationVector.z = 180;
                gameObject.transform.rotation = Quaternion.Euler(rotationVector);
            }
            
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && (direction != Vector2.up))
        {
            direction = Vector2.down;
            if (!isGameOver)
            {
                rotationVector.z = 0;
                gameObject.transform.rotation = Quaternion.Euler(rotationVector);
            }
            
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (grow)
            {
                grow = false;
                Grow();
            }

            //reverse order
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i].position = snake[i - 1].position;
            }

            Vector2 position = transform.position;
            position += (Vector2)direction;
            position.x = Mathf.RoundToInt(position.x);
            position.y = Mathf.RoundToInt(position.y);
            transform.position = position;

            yield return new WaitForSeconds(speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            grow = true;
        }

        if (other.CompareTag("Wall"))
        {
            GameOver();
        }
    }

    private void Grow()
    {
        
        var tail = Instantiate(tailPrefab);
        snake.Add(tail.transform);
        snake[snake.Count - 1].position = snake[snake.Count - 2].position;
        SetScore();
        MakeFoodInRandomPosition();

    }

    private void MakeFoodInRandomPosition()
    {
        Vector2 newFoodPosition;

        do
        {
            var x = (int)Random.Range(1, areaLimit.x);
            var y = (int)Random.Range(1, areaLimit.y);
            newFoodPosition = new Vector2(x, y);

        } while (!CanSpawnFoodInPosition(newFoodPosition));

        food.transform.position = newFoodPosition;
        Debug.Log("x:" + food.transform.position.x + "y: " + food.transform.position.y);
    }

    private bool CanSpawnFoodInPosition(Vector2 newPosition)
    {
        foreach (var tail in snake)
        {
            var x = Mathf.RoundToInt(tail.position.x);
            var y = Mathf.RoundToInt(tail.position.y);
            if (new Vector2(x, y) == newPosition)
            {
                return false;
            }
        }

        return true;
    }

    private void GameOver()
    {
        canvas.SetActive(true);
        isGameOver = true;
        gameOverText.enabled = true;
        Debug.Log("DEAD");
        StopAllCoroutines();
    }

    public void SetScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
