using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Aryzon;

public class ReplayBtnScript : AryzonRaycastInteractable
{
    public AudioSource audioPlayer;

    protected override void Down()
    {
        if (audioPlayer.loop == true)
        {
            audioPlayer.loop = false;
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
        else
        {
            audioPlayer.loop = true;
            gameObject.GetComponent<Image>().color = new Color32(165, 109, 225, 255);
        }
    }

}
