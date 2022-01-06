using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

using Aryzon;
public class SelectFriendScript : AryzonRaycastInteractable
{
    private GameObject[] profiles;
    private bool isInitialized = false;

    protected override void Awake()
    {
        base.Awake();
        profiles = GameObject.FindGameObjectsWithTag("FriendProfile");
    }

    protected override void OnEnable()
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
        base.OnEnable();
    }

    protected override void Down()
    {
        foreach (GameObject profile in profiles)
        {
            if (profile.name.Equals("profile" + gameObject.name))
            {
                profile.SetActive(true);
            }
            else
            {
                profile.SetActive(false);
            }

        }
    }
}