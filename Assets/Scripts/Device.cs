using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

    public static bool isMobileDevice()
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_TIZEN || UNITY_WP8 || UNITY_WP8_1
        return true;
#else
        return false;
#endif
    }

    public static bool isControllerDevice()
    {
        if (isAndroidTv()) return true;
        if (isMobileDevice()) return false;
        return Input.GetJoystickNames().Length > 0;
    }

    public static bool isAndroidTv()
    {
        if(SystemInfo.deviceModel.Equals("Amazon AFTB")) return true;
 
#if UNITY_ANDROID
        return Input.GetJoystickNames().Length > 0;
        
#endif
        return false;
    }
}
