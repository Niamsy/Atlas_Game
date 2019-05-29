using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static int ToInt(this Enum e)
    {
        return Convert.ToInt32(e);
    }
}
