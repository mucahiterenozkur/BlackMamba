using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    Snake snake;

    // Start is called before the first frame update
    void Start()
    {
        snake = FindObjectOfType<Snake>();
        animator = GetComponent<Animator>();
        animator.SetBool("isGameOver", false);
        animator.SetBool("isGameOver2", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (snake.isGameOver)
        {
            animator.SetBool("isGameOver", true);
            animator.SetBool("isGameOver2", true);
        }
    }
}
