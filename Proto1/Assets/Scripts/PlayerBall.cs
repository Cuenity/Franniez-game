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
    public bool canJump = true;
    private float timeSinceJump;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //faka buttons
        //startButton.onClick.AddListener(StartLevel);
       
    }


    void Update()
    {
        if (!canJump)
        {
            timeSinceJump += Time.deltaTime;  // only need to increase if canJump == flase
        }

        if (timeSinceJump >= 2.5f)
        {
            canJump = true;
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
            Debug.Log("Jump");
            canJump = false;
            timeSinceJump = 0.0f;
        }
    }

    public void StopMoving()
    {
        rb.velocity = new Vector3(0f,0f,0f);
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
            text.text = "JE HEBT VERLOREN";
        }
        if (collision.collider.CompareTag("Finish"))
        {
            text.text = "JE HEBT GEWONNEN";
        }
    }

    //dit werkt niet a.k.a. faka buttons
    void StartLevel()
    {
        Debug.Log("shitbutton");
        rb.isKinematic = false ;
    }
    
}