using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PickObject : MonoBehaviour
{
    Camera _currentCamera;

       AudioSource _currentSource;

   
    Ray _lastRayCast;
    float _raycastDistance = 1.0f;

  
    void Start()
    {
        // Get the Camera and AudioSource components on the current GameObject.
        _currentCamera = GetComponent<Camera>();
        _currentSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_lastRayCast.origin, _lastRayCast.direction * _raycastDistance, Color.red);

        // Select an object based on mouse click.
        // button 0 is the left mouse button.
        if (Input.GetMouseButtonDown(0) == true)
        {
           
            Ray clickRay = _currentCamera.ScreenPointToRay(Input.mousePosition);
            _lastRayCast = clickRay;

     
            _currentSource.Play();

         
            RaycastHit hit;

           
            bool didHit = Physics.Raycast(clickRay, out hit);
            if (didHit)
            {
                Debug.Log("Hey! We hit something!");


                GameObject objectWeHit = hit.transform.gameObject;

           
                AudioSource objectWeHitAudio = objectWeHit.GetComponent<AudioSource>();
                if (objectWeHitAudio != null)
                {
                    // If so, play it!
                    objectWeHitAudio.Play();
                }

               
                _raycastDistance = hit.distance;

                Collider[] allObjectsWithinSphere = Physics.OverlapSphere(hit.point, 1.8f);
                foreach (Collider collider in allObjectsWithinSphere)
                {
                    Rigidbody colliderRigidbody = collider.GetComponent<Rigidbody>();

                    // Apply a force where the ray hit the object.
                    if (colliderRigidbody != null)
                    {
                        // If the object isn't using gravity, tell it to.
                        colliderRigidbody.useGravity = true;

                        // Spawn an explosion where the ray hit the object.
                        colliderRigidbody.AddExplosionForce(5000.0f, hit.point, 10.25f);
                    }
                }

            }
        }
    }
}
