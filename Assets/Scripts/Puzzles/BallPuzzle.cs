using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPuzzle : MonoBehaviour
{
    [Header("Singleton")]
    [Header("Puzzle Components")]
    [SerializeField] private bool chestOpen;
    [SerializeField] [Range(1, 100)] private int triggerCount = 3;
    [SerializeField] private GameObject orbitingPrefab;
    [SerializeField] [Range(1, 10)] private float orbitSpeed = 1f;

    private List<GameObject> placementObjectsList;

    private int followIndex = 0;
    private GameObject orbitingObject = null;

    //Singleton
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
        if(chestOpen == true && placementObjectsList != null && placementObjectsList.Count >= triggerCount)
        {
            if (orbitingObject == null)
                orbitingObject = Instantiate(orbitingPrefab, placementObjectsList[0].transform.position, placementObjectsList[0].transform.rotation, transform);
            else
                orbitingObject.SetActive(true);

            //Rotate between objects
            orbitingObject.transform.position = Vector3.MoveTowards(orbitingObject.transform.position, placementObjectsList[followIndex].transform.position, orbitSpeed * Time.deltaTime);
            if (orbitingObject.transform.position == placementObjectsList[followIndex].transform.position)
            {
                followIndex++;

                if (followIndex >= placementObjectsList.Count)
                    followIndex = 0;
            }
        }
        else 
        {
            if (orbitingObject != null)
                orbitingObject.SetActive(false);
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
