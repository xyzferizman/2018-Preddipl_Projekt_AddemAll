using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    
    Direction currentDir;
    Vector2 input;
    bool isMoving = false;
    float t;
    Rigidbody2D rb2d;
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    public float walkSpeed = 3f;

    public bool isAllowedToMove = true;

    void Start()
    {
        isAllowedToMove = true;
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
    }

    void Update()
    {

        if (!isMoving && isAllowedToMove)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if (input != Vector2.zero)
            {

                if (input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if (input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch (currentDir)
                {
                    case Direction.North:
                        gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        break;
                    case Direction.East:
                        gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        break;
                    case Direction.South:
                        gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        break;
                    case Direction.West:
                        gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        break;
                }
                Move();
            }

        }

    }

    public void Move()
    {
        isMoving = true;
        t = 0;
        Vector2 startPos = rb2d.position; 
        Vector2 endPos = new Vector2(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y));
        rb2d.MovePosition(endPos*walkSpeed);

        isMoving = false;
    }
}

enum Direction
{
    North,
    East,
    South,
    West
}
