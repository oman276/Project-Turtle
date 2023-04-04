using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : Button
{
    CameraControls cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = FindObjectOfType<CameraControls>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        print("pressed");
        cc.MapButtonOn();
    }

    // Button is released
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        print("released");
        cc.MapButtonOff();
    }

}
