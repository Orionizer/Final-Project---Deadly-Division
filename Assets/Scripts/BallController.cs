using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class BallController : MonoBehaviour
{
    [SerializeField] float ballSpeedX = 0f; //Xspeed of the ball
    [SerializeField] float ballSpeedY = 0f; //Yspeed of the ball
    Rigidbody2D Rb; //calls rb for ball
    Vector2 moveVector; //calls to move vecotr
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>(); //grabs rb
    }

    void Update()
    {
        Move();
    }
    void Move() //Movement script making the ball do its default "move right" will be replaced later with a rest phase till hit
    {
        moveVector = new Vector2(Rb.position.x + ballSpeedX * Time.deltaTime, Rb.position.y - ballSpeedY * Time.deltaTime); //is constantly called in update and makes the ball move in the movevectory direction based on the calculations of position x and y
        Rb.MovePosition(moveVector); //moves vector of rb in the desired direction above
    }
    private void OnCollisionEnter2D(Collision2D collision) //test collisions making the ball bounce the oposite direction on contact (ie right makes it go left)
        //Improvment note replace the -1 multiplication with a variable to avoid magic numbers and to tweak later
    {
        if (collision.gameObject.tag == "Wall R")
        {
            ballSpeedX *= -1;
        }
        if (collision.gameObject.tag == "Wall L")
        {
            ballSpeedX *= -1;
        }
        if (collision.gameObject.tag == "Floor")
        {
            ballSpeedY *= -1;
        }
        if (collision.gameObject.tag == "Ceiling")
        {
            ballSpeedY *= -1;
        }
        if(collision.gameObject.tag == "Attack")
        {
            ballSpeedX += -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("ouch");
        }
    }

}
