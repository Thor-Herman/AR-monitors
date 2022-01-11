using UnityEngine;
using UnityEngine.Events;

namespace Aryzon
{
    // This class shows how to receive and handle reticle  events in your code.
    // Place this component on an object with a renderer, like a cube and its
    // material will change color when the reticle hovers over it.
    // It is recommended to duplicate and edit this code in your own class.

    //[RequireComponent(typeof(AryzonRaycastObject))]
    public class AryzonRaycastInteractable : MonoBehaviour
    {
        protected AryzonRaycastObject raycastObject;

        protected virtual void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
        }

        protected virtual void OnEnable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.AddListener(Off);
                raycastObject.OnPointerOver.AddListener(Over);
                raycastObject.OnPointerUp.AddListener(Clicked);
                raycastObject.OnPointerDown.AddListener(Down);
            }
        }

        protected virtual void OnDisable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.RemoveListener(Off);
                raycastObject.OnPointerOver.RemoveListener(Over);
                raycastObject.OnPointerUp.RemoveListener(Clicked);
                raycastObject.OnPointerDown.RemoveListener(Down);
            }
        }

        protected virtual void Off()
        {
            
        }

        protected virtual void Over()
        {

        }

        protected virtual void Down()
        {

        }

        protected virtual void Up() {

        }

        protected virtual void Clicked()
        {

        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mouse")) // Called when user clicks on this part
            {
                Down();
            }
            else if (other.CompareTag("MouseUp")) {
                Up();
            }
        }
    }
}