using UnityEngine;

public class ENTITY : MonoBehaviour
{
    public ENVIRONMENT_Room location {private set; get;}

    public void UpdateLocation(ENVIRONMENT_Room newLocation){ location = newLocation; }
}