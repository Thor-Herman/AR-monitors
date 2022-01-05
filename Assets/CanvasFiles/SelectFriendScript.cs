using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Aryzon
{
    public class SelectFriendScript : MonoBehaviour
    {
        private AryzonRaycastObject raycastObject;
        private GameObject[] profiles;
        private bool isInitialized = false;

        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
            profiles =  GameObject.FindGameObjectsWithTag("FriendProfile");          
        }
        
        private void OnEnable()
        {
            if (isInitialized)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (profiles[i].name.Equals("profile" + gameObject.name))
                    {
                        profiles[i].SetActive(false);
                    }
                }
            }
            else
            {
                isInitialized = true;
            }
            if (raycastObject)
            {
                raycastObject.OnPointerOff.AddListener(Off);
                raycastObject.OnPointerOver.AddListener(Over);
                raycastObject.OnPointerUp.AddListener(Clicked);
                raycastObject.OnPointerDown.AddListener(Down);
            }
        }

        private void OnDisable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.RemoveListener(Off);
                raycastObject.OnPointerOver.RemoveListener(Over);
                raycastObject.OnPointerUp.RemoveListener(Clicked);
                raycastObject.OnPointerDown.RemoveListener(Down);
            }
        }

        private void Off()
        {

        }

        private void Over()
        {

        }

        private void Down()
        {
            foreach( GameObject profile in profiles)
            {
                if(profile.name.Equals("profile" + gameObject.name))
                {
                    profile.SetActive(true);
                }
                else
                {
                    profile.SetActive(false);
                }
                
            }
        }

        private void Clicked()
        {

        }
    }
}