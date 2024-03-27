using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private float rayLength = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject objectContainer;
    [Space(10)]

    [Header("Placement Object")]
    [SerializeField] public GameObject placementObject;
    [SerializeField] private int maxObjects = 3;
    private List<GameObject> objectList = new List<GameObject>();

    private void Awake() {
        
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
                        Debug.Log("Chest Controller");
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