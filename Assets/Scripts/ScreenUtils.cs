using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtils
{
    //public static ScreenUtils instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}

    public static Vector2 GetCenterOfScreen()
    {
        float x = Screen.width / 2f;
        float y = Screen.height * 0.5f;

        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            return new Vector2(y, x);
        }

        return new Vector2(x, y);
    }
}
