using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanePositioning : MonoBehaviour
{
    [SerializeField] private GameObject tableTransform;
    [SerializeField] private Transform planePosition;
    [SerializeField] private Transform chrysolEmptyPos;
    [SerializeField] private GameObject chrysolPosition;
    private Rigidbody chrysolito;
    [SerializeField] private ARRaycastManager _raycastMng;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private ARPlane plane;

    // Start is called before the first frame update
    void Start()
    {
        chrysolito = chrysolPosition.GetComponent<Rigidbody>();
        //TODO Detect screen tap to start simulation; timeDeltaTime = 0.

        //_raycastMng = GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {


        /*if(Input.GetTouch())
        {
            //TODO StartSimulation();
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(Input.touchCount -1);

            if (touch.phase == TouchPhase.Began)
            {
                if (_raycastMng.Raycast(touch.position, _hits, TrackableType.Planes))
                {
                    ARRaycastHit hit = _hits[0];
                    //ARPlane plane = hit.trackable as ARPlane;
                    
                    planePosition.position = hit.pose.position;
                    chrysolEmptyPos.transform.position = planePosition.position;
                    planePosition.rotation = hit.pose.rotation;
                    chrysolEmptyPos.transform.rotation = planePosition.rotation;

                    StartSimulation();
                }
            }



        }
    }

    void StartSimulation()
    {
        chrysolito.AddForce(chrysolPosition.transform.forward * 5f, ForceMode.Impulse);
    }
}
