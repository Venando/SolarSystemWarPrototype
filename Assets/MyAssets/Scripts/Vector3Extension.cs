using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3ExtensionMethods
{
    public static string ToParsableString(this Vector3 v) => v.x.ToString() + "," + v.y.ToString() + "," + v.z.ToString();

    public static Vector3 ToVector3(this string str)
    {
        var parts = str.Split(',');
        return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
    }
}
