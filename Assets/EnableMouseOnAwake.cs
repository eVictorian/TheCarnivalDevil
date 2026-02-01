using UnityEngine;

public class EnableMouseOnAwake : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = true;          // show the cursor
        Cursor.lockState = CursorLockMode.None;  // free movement
    }
}
