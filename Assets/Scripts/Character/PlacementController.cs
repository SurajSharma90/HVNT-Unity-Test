using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] [Range(10, 100)] private float rayLength = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private GameObject objectContainer = null;
    [Space(10)]

    [Header("Placement Object")]
    [SerializeField] public GameObject placementObject;
    [SerializeField] private int maxObjects = 3;
    private List<GameObject> objectList = new List<GameObject>();

    private void Awake() 
    {
        if(mainCamera == null)
            Debug.LogException(new System.NullReferenceException("Main Camera object not assigned."));
        if (objectContainer == null)
            Debug.LogException(new System.NullReferenceException("Object Container object not assigned."));
        if (placementObject == null)
            Debug.LogException(new System.NullReferenceException("Placement Object object not assigned."));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayLength, layerMask)) {
                
                if(hit.transform.tag == "Chest")
                {
                    ChestController chestController;
                    hit.transform.gameObject.TryGetComponent(out chestController);

                    if(chestController) 
                    {
                        chestController.ToggleOpenLid();
                    }
                }
                else 
                {
                    if(objectList.Count >= maxObjects) 
                    {
                        Destroy(objectList[0]);
                        objectList.RemoveAt(0);

                        GameObject newObject = Instantiate(placementObject, hit.point, Quaternion.identity, objectContainer.transform);
                        objectList.Add(newObject);
                    }
                    else 
                    {
                        GameObject newObject = Instantiate(placementObject, hit.point, Quaternion.identity, objectContainer.transform);
                        objectList.Add(newObject);
                    }

                    BallPuzzle.instance.SetPlacementObjectsList(objectList);
                }
            }
        }
    }
}
