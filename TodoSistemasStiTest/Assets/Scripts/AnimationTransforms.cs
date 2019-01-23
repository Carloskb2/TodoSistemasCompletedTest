using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AnimationTransforms
{
    public List<AnimationPart> Parts = new List<AnimationPart>();
}

[System.Serializable]
public class AnimationPart
{
    public string BodyPart;
    public Vector3 Position;
    public long FrameNumber;
}
