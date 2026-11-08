using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class BallController : MonoBehaviour
{
    [SerializeField] float angle = 0f; //angle at which the ball goes
    [SerializeField] float ballSpeed = 0f; // total speed of the ball
    float ballAngleX = 0f; //Xspeed of the ball
    float ballAngleY = 0f; //Yspeed of the ball
    Rigidbody2D Rb; //calls rb for ball
    Vector2 moveVector; //calls to move vecotr
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>(); //grabs rb
        ChangeAngle(angle);
    }

    void Update()
    {
        Move();
    }
    void Move() //Movement script making the ball do its default "move right" will be replaced later with a rest phase till hit
    {
        moveVector = new Vector2(ballAngleX * ballSpeed, ballAngleY * ballSpeed); //is constantly called in update and makes the ball move in the movevectory direction based on the calculations of position x and y
        Rb.linearVelocity = moveVector; //moves vector of rb in the desired direction above
    }
    private void OnCollisionEnter2D(Collision2D collision) //test collisions making the ball bounce the oposite direction on contact (ie right makes it go left)
        //Improvment note replace the -1 multiplication with a variable to avoid magic numbers and to tweak later
    {
        if (collision.gameObject.tag == "Wall R")
        {
            ballAngleX *= -1;
        }
        if (collision.gameObject.tag == "Wall L")
        {
            ballAngleX *= -1;
        }
        if (collision.gameObject.tag == "Floor")
        {
            ballAngleY *= -1;
        }
        if (collision.gameObject.tag == "Ceiling")
        {
            ballAngleY *= -1;
        }
        if(collision.gameObject.tag == "Attack")
        {
            ballAngleX += -1;
        }
        if (collision.gameObject.tag == "Player")
        {
            print("ouch");
            ChangeAngle(angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("ouch");
            ChangeAngle(angle);
        }
    }
    private void ChangeAngle(float angle)
    {
        ballAngleX = Mathf.Sin(angle * Mathf.Deg2Rad);
        ballAngleY = Mathf.Cos(angle * Mathf.Deg2Rad);
        print(ballAngleX);
        print(ballAngleY);
    }

}
