using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPuzzle : MonoBehaviour
{
    [Header("Puzzle Components")]
    [SerializeField] private bool chestOpen;
    [SerializeField] private List<GameObject> placementObjectsList;
    [SerializeField] private int triggerCount = 3;
    [SerializeField] private GameObject orbitingPrefab;

    private GameObject orbitingObject = null;

    public static BallPuzzle instance;

    private void Awake() 
    {
        instance = this;
        
        if (orbitingPrefab == null)
            Debug.LogException(new System.NullReferenceException("Orbiting Prefab is not assigned."));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(chestOpen == true && placementObjectsList.Count >= triggerCount) 
        {
            if (orbitingObject == null)
                orbitingObject = Instantiate(orbitingPrefab, placementObjectsList[0].transform.position, placementObjectsList[0].transform.rotation, transform);
        }
    }

    public void SetChestOpen(bool open) 
    {
        chestOpen = open;
    }

    public void SetPlacementObjectsList(List<GameObject> list) 
    {
        placementObjectsList = list;
    }
}
