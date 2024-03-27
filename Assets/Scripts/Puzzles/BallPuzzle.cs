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
    [SerializeField] [Range(0, 10)] private float orbiterOffset = 0.5f;

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
            Vector3 positionOffset = placementObjectsList[0].transform.up.normalized * orbiterOffset;
            if (orbitingObject == null)
                orbitingObject = Instantiate(orbitingPrefab, placementObjectsList[0].transform.position + positionOffset, placementObjectsList[0].transform.rotation, transform);
            else
                orbitingObject.SetActive(true);

            //Rotate between objects
            positionOffset = placementObjectsList[followIndex].transform.up.normalized * orbiterOffset;
            orbitingObject.transform.position = Vector3.MoveTowards(orbitingObject.transform.position, placementObjectsList[followIndex].transform.position + positionOffset, orbitSpeed * Time.deltaTime);
            if (orbitingObject.transform.position == placementObjectsList[followIndex].transform.position + positionOffset)
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

    public void ResetOrbitingObject() 
    {
        if(orbitingObject != null && placementObjectsList != null && placementObjectsList.Count >= triggerCount) 
        {
            Vector3 positionOffset = placementObjectsList[0].transform.up.normalized * orbiterOffset;
            orbitingObject.transform.position = placementObjectsList[0].transform.position + positionOffset;
            orbitingObject.transform.rotation = placementObjectsList[0].transform.rotation;
        }
    }
}
