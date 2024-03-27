using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField] private GameObject lid = null;
    [SerializeField] private GameObject body = null;
    [Space(10)]

    [Header("Animation")]
    [SerializeField] private Animator chestAnimator = null;
    [SerializeField] private float openAngle = -70f;
    [SerializeField] private bool open = false;

    private void Awake() {
        if (lid == null || body == null)
            Debug.LogError("lid or body objects not assigned.");

        if (chestAnimator == null)
            Debug.LogError("animator object not assigned.");
    }

    // Start is called before the first frame update
    void Start()
    {
        lid.transform.localRotation = Quaternion.Euler(openAngle, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOpenLid() 
    {
        open = !open;
        chestAnimator.SetBool("chest_open", open);
        BallPuzzle.instance.SetChestOpen(open);
    }
}
