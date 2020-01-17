﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMath
{
    public static float Lerp(float min, float max, float p)
    {
        return (max - min) * p + min;
    }

    public static Vector3 Lerp(Vector3 min, Vector3 max, float p)
    {
        float x = Lerp(min.x, max.x, p);
        float y = Lerp(min.y, max.y, p);
        float z = Lerp(min.z, max.z, p);
        return new Vector3(x, y, z);
    }

    public static float Smooth(float min, float max, float p)
    {
        p = p * p * (3 - 2 * p);
        return (max - min) * p + min;
    }

    public static float Dampen(float current, float target, float percentAfter1Second)
    {
        return Lerp(current, target, 1 - Mathf.Pow(percentAfter1Second, Time.deltaTime));
    }

    public static Vector3 Dampen(Vector3 current, Vector3 target, float percentAfter1Second)
    {
        float p = 1 - Mathf.Pow(percentAfter1Second, Time.deltaTime);
        return Lerp(current, target, p);
    }

}