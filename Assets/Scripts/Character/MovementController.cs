using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody playerRigidBody = null;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform orientation;

    private Vector3 direction = Vector3.zero;
    //[Space(10)]

    //[Header("Mouse")]
    

    private void Awake() 
    {
        if (playerRigidBody == null)
            Debug.LogError("Player Rigid Body is not assigned.");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        playerRigidBody.velocity = direction.normalized * speed;
    }
}
