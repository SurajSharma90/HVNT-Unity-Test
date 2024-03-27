using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookController : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector2 sensitivity = Vector2.one;
    [SerializeField] private float minClamp, maxClamp;
    [SerializeField] public Transform orientation;

    private Vector2 rotation = Vector2.zero;

    private void Awake() 
    {
        if (mainCamera == null)
            Debug.LogException(new System.NullReferenceException("Main Camera object not assigned."));
        if (orientation == null)
            Debug.LogException(new System.NullReferenceException("Orientation object not assigned."));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)) {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity.x;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity.y;

            rotation.y += mouseX;
            rotation.x -= Mathf.Clamp(mouseY, minClamp, maxClamp);

            mainCamera.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
            orientation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
