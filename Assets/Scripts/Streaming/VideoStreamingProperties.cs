using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VideoStreamingProperties
{
    public VideStreamingProperty[] videoStreamingProperties;
}

[Serializable]
public class VideStreamingProperty
{
    public int playerIndex;
    public string url;
}