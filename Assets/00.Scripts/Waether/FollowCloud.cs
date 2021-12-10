using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CloudColor
{
    public int startTime;
    public int endTime;

    public Color destinyColor;

    public bool CheckCloudTime(int hour)
    {
        if (startTime <= hour && endTime > hour)
            return true;
        else
            return false;
    }
}

public class FollowCloud : MonoBehaviour
{
    Transform TargetTransform;

    Renderer LowRenderer;
    Renderer HighRenderer;

    public List<CloudColor> lowColordest;
    public List<CloudColor> highColordest;

    WorldManager w_manager;

    private void Start()
    {
        TargetTransform = GameObject.FindObjectOfType<PlayerController>().transform;

        LowRenderer = transform.GetChild(0).GetComponent<Renderer>();
        HighRenderer = transform.GetChild(1).GetComponent<Renderer>();

        w_manager = WorldManager.Instance;
    }

    private void FixedUpdate()
    {
        transform.position = TargetTransform.position;

        CloudColor();
    }

    void CloudColor()
    {
        var isNight = w_manager.GetIsNight();
        float totaltime = w_manager.GetTotalTime();

        int hour = w_manager.hour;
        int minute = w_manager.minute;
        float second = w_manager.second;

        for(int i = 0; i < lowColordest.Count; i++)
        {
            if(lowColordest[i].CheckCloudTime(hour))
            {
                Color beforeColor = Color.white;

                if(i != 0 && lowColordest.Count != 1)
                    beforeColor = lowColordest[i - 1].destinyColor;
                else if(lowColordest.Count != 1)
                    beforeColor = lowColordest[lowColordest.Count - 1].destinyColor;

                float timeGap = lowColordest[i].endTime - lowColordest[i].startTime;
                float totalTime = ((hour - lowColordest[i].startTime) * 60 * 60) + (minute * 60) + second;

                LowRenderer.material.SetColor("_CloudColor", Color.Lerp(beforeColor, lowColordest[i].destinyColor, totalTime / (timeGap * 60 * 60)));
            }
        }

        for (int i = 0; i < highColordest.Count; i++)
        {
            if (highColordest[i].CheckCloudTime(hour))
            {
                Color beforeColor = Color.white;

                if (i != 0 && highColordest.Count != 1)
                    beforeColor = highColordest[i - 1].destinyColor;
                else if (highColordest.Count != 1)
                    beforeColor = highColordest[highColordest.Count - 1].destinyColor;

                float timeGap = highColordest[i].endTime - highColordest[i].startTime;
                float totalTime = ((hour - highColordest[i].startTime) * 60 * 60) + (minute * 60) + second;

                HighRenderer.material.SetColor("_CloudColor", Color.Lerp(beforeColor, highColordest[i].destinyColor, totalTime / (timeGap * 60 * 60)));
            }
        }

        //if (isNight)
        //{
        //    float totaltimeTemp = 0;

        //    // 밤
        //    if (hour >= 18)
        //        totaltimeTemp = ((hour - 18) * 60 * 60) + (minute * 60) + second;
        //    else
        //        totaltimeTemp = (6 * 60 * 60) - ((hour * 60 * 60) + (minute * 60) + second);

        //    LowRenderer.material.SetColor("_CloudColor", Color.Lerp(LowCloudOriginColor, Color.black, totaltimeTemp / (6 * 60 * 60)));
        //    HighRenderer.material.SetColor("_CloudColor", Color.Lerp(HighCloudOriginColor, Color.black, totaltimeTemp / (6 * 60 * 60)));
        //}
        //else
        //{
        //    // 낮
        //    if(hour <= 12)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}
    }
}
