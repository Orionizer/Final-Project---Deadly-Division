using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float ballSpeed = 0f;
    Rigidbody2D myRigidbody;
    Vector2 moveVector;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        moveVector = new Vector2(myRigidbody.position.x + ballSpeed, myRigidbody.position.y);
        myRigidbody.position.x = moveVector;
    }
}
