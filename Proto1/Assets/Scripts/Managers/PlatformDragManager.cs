using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDragManager : MonoBehaviour
{
    private Vector3 screenPoint; private Vector3 offset;

    private Camera camera;

    void Start()
    {
    }


    void Update()
    {

    }



    void OnMouseDown()
    {
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        screenPoint = camera.WorldToScreenPoint(transform.position);

        GameState.Instance.playerCamera.platformDragActive = true;

        //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = camera.ScreenToWorldPoint(curScreenPoint);// + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        GameState.Instance.levelManager.playerPlatforms.placedPlatforms.Add(gameObject);

        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

        GameState.Instance.platformManager.spawnPlatformOnGrid(transform.position, gameObject);
        GameState.Instance.playerCamera.platformDragActive = false;
    }
}