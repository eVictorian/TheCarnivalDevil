using UnityEngine;
using UnityEngine.Events;

public class ENTITY : MonoBehaviour
{
    public UnityEvent onDeath;

    public ENVIRONMENT_Room location {private set; get;}

    public soDATA_GameEvent onDeathRaise;

    public void UpdateLocation(ENVIRONMENT_Room newLocation){ location = newLocation; }

    public void Die(){ if (onDeathRaise != null){ onDeathRaise.Raise(); } onDeath.Invoke();  }
}