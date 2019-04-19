using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public PlatformType type;

    private void OnMouseDown()
    {
        switch (type)
        {
            case PlatformType.ramp:
                MirrorPlatform();
                break;
            case PlatformType.platformSquare:
                // op zijn zeikant 90 draaien    zoiets? gameObject.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, startRotation + 90, 0));
                break;
            case PlatformType.trampoline:
                // niets
                break;
            case PlatformType.boostPlatform:
                // omdraaien, maar dan moet de code ook echt werken zodat hij dan de andere kant op vliegt
                break;
            case PlatformType.cannon:
                // of volledige vrijheid in draaien of 0, 45, 90, 135, 180 graden.
                break;
            default:
                break;
        }
    }

    private void MirrorPlatform()
    {
        gameObject.transform.parent.transform.localRotation = new Quaternion(
                gameObject.transform.parent.transform.localRotation.x * -1.0f,
                gameObject.transform.parent.transform.localRotation.y,
                gameObject.transform.parent.transform.localRotation.z,
                gameObject.transform.parent.transform.localRotation.w * -1.0f);
        gameObject.transform.localPosition = new Vector3(
                gameObject.transform.localPosition.x * -1.0f,
                gameObject.transform.localPosition.y * -1.0f,
                gameObject.transform.localPosition.z); //= new Vector3(-0.0101f, 0, 0);

        //gameObject.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
    }
}