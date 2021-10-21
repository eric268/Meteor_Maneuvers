using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper : MonoBehaviour
{
    public static float CalculateAngle(Vector2 vec1, Vector2 vec2)
    {
        float det = vec1.x * vec2.y - vec1.y * vec2.x;

        return Mathf.Atan2(det, Vector2.Dot(vec1, vec2))*Mathf.Rad2Deg;
    }
}
