using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable { public void Interact(); }
public class Interaction : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public GameObject InteractPrompt;

    private void Start()
    {
        InteractPrompt.SetActive(false);    
    }
    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                InteractPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                    Cursor.lockState = CursorLockMode.None;
                    InteractPrompt.SetActive(false);
                }
            }

        }
        else
            InteractPrompt.SetActive(false);
    }
}
