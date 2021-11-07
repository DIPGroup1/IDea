using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public BuildType buildType;
    public string buildId;
    //public string ip;
    //public ushort port;

}

public enum BuildType
{
    REMOTE_SERVER,
    REMOTE_CLIENT
}
