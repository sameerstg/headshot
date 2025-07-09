using Photon.Pun;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Texture2D crosshair;
    PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.ForceSoftware);
        } 
    }

}
