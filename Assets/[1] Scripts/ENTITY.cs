using UnityEngine;
using UnityEngine.Events;

public class ENTITY : MonoBehaviour
{
    public UnityEvent onDeath;

    public ENVIRONMENT_Room location {private set; get;}

    public void UpdateLocation(ENVIRONMENT_Room newLocation){ location = newLocation; }

    public void Die(){ onDeath.Invoke(); }
}