using UnityEngine;

public class QUATERNION_Fixers 
{
    public static Quaternion Safe(Quaternion q)
    {
        if (float.IsNaN(q.x) || float.IsNaN(q.y) || float.IsNaN(q.z) || float.IsNaN(q.w))
            return Quaternion.identity;

        if (q == new Quaternion(0,0,0,0))
            return Quaternion.identity;

        return q;
    }

}