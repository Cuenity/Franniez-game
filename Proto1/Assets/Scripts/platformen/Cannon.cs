using UnityEngine;

public class Cannon : Platform
{
    //public Canvas sliderCanvas;
    //private Canvas slider;

    private void Start()
    {
        transform.GetChild(4).GetComponent<Canvas>().worldCamera = GameState.Instance.playerCamera.GetComponent<Camera>();

        //slider = Instantiate(sliderCanvas);
        //slider.transform.SetParent(transform);
        //slider.transform.GetChild(0).transform.localScale = transform.localScale;
        //slider.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.GetComponent<PlayerBallManager>())
        {
            GameObject playerBall = collision.gameObject;
            gameObject.GetComponentInChildren<CannonFirePoint>().FireCannon(playerBall, this);
        }
    }
}