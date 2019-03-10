using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBall : MonoBehaviour
{
    private Rigidbody rb;
    //faka buttons
    //public Button startButton;

    public Text text;

    private Vector3 lastTouchedPlatform;
    private float timeSinceJump;

    public bool isAlive = true;
    public bool canJump = true;
    public bool canBoost = true;
    public bool playerHitGoal = false;

    public int points =0;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //faka buttons
        //startButton.onClick.AddListener(StartLevel);
       
    }


    void Update()
    {
        if (!canJump | !canBoost)
        {
            timeSinceJump += Time.deltaTime;  // only need to increase if canJump == flase
        }
        //boost in zelfde methode
        if (timeSinceJump >= 2.5f)
        {
            canJump = true;
            canBoost = true;
        }
        //(verplaatst naar GameManager)
        //DAN MAAR GEWOON MET Q
        //if (Input.GetKeyDown("q"))
        //{
        //    rb.isKinematic = false;
        //}

        //tekst wel maar ho stop maar als je een button event wil gebruiken zoals beschreven in de unity dcumentatie dat werkt niet hoor oh nee
        //if (Input.GetKeyDown("i"))
        //{
        //    text.text = "DOE DAN";
        //}
        //-.-
    }

    public void Jump()
    {
        if (canJump)
        {
            rb.AddForce(Vector3.up * 500f); 
            canJump = false;
            timeSinceJump = 0.0f;
        }
    }

    public void StopMoving()
    {
        //Killt alle velocity kan wel es rare dingen doen
        rb.velocity = new Vector3(0f,0f,0f);
    }

    public void IncreaseSpeed()
    {
        if (canBoost)
        {
            Vector3 newVelocity;
            //moet uitzoeken of balletje links of rechts gaat
            if (rb.velocity.x >= 0) {
                newVelocity = new Vector3(rb.velocity.x + 10f, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                newVelocity = new Vector3(rb.velocity.x - 10f, rb.velocity.y, rb.velocity.z);
            }
            rb.velocity = newVelocity;
            canBoost = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //platform interactie
        Vector3 goHardOrGoHome = new Vector3(0, 50, 0);

        //abilitiecode
        lastTouchedPlatform = collision.collider.transform.position;
        Debug.Log("collisontrigger");


        if (collision.collider.CompareTag("Bounce"))
        {
            Debug.Log("bouncersboncers");
            rb.AddForce(goHardOrGoHome,ForceMode.Impulse);
        }

        //voor obstakels
        if (collision.collider.CompareTag("Kill"))
        {
            Destroy(this.gameObject);
            isAlive = false;
        }
        if (collision.collider.CompareTag("Finish"))
        {
            text.text = "JE HEBT GEWONNEN";
            playerHitGoal = true;

        }
        if (collision.collider.CompareTag("Pick-Up"))
        {
            points++;
            Destroy(collision.collider.gameObject);
        }
    }

    

    //dit werkt niet a.k.a. faka buttons
    void StartLevel()
    {
        Debug.Log("shitbutton");
        rb.isKinematic = false ;
    }
    
}