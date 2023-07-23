using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Door door;

    public void OpenDoor()
    {
        door.Open();
    }

    public void CloseDoor()
    {
        door.Close();
    }
}
