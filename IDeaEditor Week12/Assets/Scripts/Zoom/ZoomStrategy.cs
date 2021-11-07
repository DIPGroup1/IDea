using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZoomStrategy 
{
    void Zoomin(Camera cam, float delta, float nearZoomLimit);
    void Zoomout(Camera cam, float delta, float farZoomLimit);
}
