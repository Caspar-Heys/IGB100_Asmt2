using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform PlayerCamera;
    public Vector2 Sensitivities;

    public Vector3 XYRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Player>().GetTeleportTimer() == 0)
        {
            Look();
        }
    }

    private void Look()
    {
        Vector2 MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        XYRotation.x -= MouseInput.y * Sensitivities.y;
        XYRotation.y += MouseInput.x * Sensitivities.x;

        XYRotation.x = Mathf.Clamp(XYRotation.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, XYRotation.y, 0f);

        PlayerCamera.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);
    }

    public void SetRotation(Transform t)
    {
        XYRotation = t.rotation.eulerAngles;
        transform.eulerAngles = new Vector3(0f, XYRotation.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);
    }
}
