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
    [SerializeField] private bool open = false;

    private void Awake() {
        if (lid == null || body == null)
            Debug.LogException(new System.NullReferenceException("lid or body objects not assigned."));
        if (chestAnimator == null)
            Debug.LogException(new System.NullReferenceException("animator object not assigned."));
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void Reset() 
    {
        open = false;
        chestAnimator.SetBool("chest_open", false);
        BallPuzzle.instance.SetChestOpen(false);
    }
}
