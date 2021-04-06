using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;

    private void Start()
    {
        if (GameManager.Instance.Players[0] == null)
        {
            GameManager.Instance.Players[0] = this.gameObject;
            this.gameObject.name = "Player 1";
        }
        else
        {
            GameManager.Instance.Players[1] = this.gameObject;
            this.gameObject.name = "Player 2";
        }

        // Check to see if the game is multiplayer
        if (GameManager.Instance.isMultiplayer)
        {
            if (this.gameObject == GameManager.Instance.Players[0])
            {
                playerCamera.rect = new Rect(0f, 0f, 0.5f, 1f);
            }
            else
            {
                playerCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            }
        }
        else
        {
            // Do nothing
        }
    }
}
