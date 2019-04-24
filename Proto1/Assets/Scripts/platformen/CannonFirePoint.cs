using System.Collections;
using UnityEngine;

public class CannonFirePoint : MonoBehaviour
{
    private GameObject playerBall;

    [SerializeField] private float waitingTimeTillFiring;
    [SerializeField] private float fireSpeed;

    public void FireCannon(GameObject playerBall)
    {
        //waitingTimeTillFiring = 1;
        //fireSpeed = 10;

        this.playerBall = playerBall;
        playerBall.SetActive(false);
        playerBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        playerBall.transform.position = gameObject.transform.position;
        StartCoroutine(FireAfterTime());
    }

    private IEnumerator FireAfterTime()
    {
        yield return new WaitForSeconds(waitingTimeTillFiring);

        StartCoroutine(DisableColliderForTime());
        float transformUp = gameObject.transform.up.x;
        //transformUp (/15)
        //playerBall.transform.position = transform.position + gameObject.transform.parent.forward;
        playerBall.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.transform.up.x, gameObject.transform.up.y, 0) * -fireSpeed; // + new Vector3(-10, 0, 0)
        //playerBall.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.transform.parent.rotation.x, gameObject.transform.parent.rotation.y, gameObject.transform.parent.rotation.z);
        playerBall.SetActive(true);
    }

    private IEnumerator DisableColliderForTime()
    {
        transform.root.gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.2f);

        transform.root.gameObject.GetComponent<Collider>().enabled = true;
    }
}