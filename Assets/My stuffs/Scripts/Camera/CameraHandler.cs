
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


namespace felipsteles
{
    public class CameraHandler : MonoBehaviour
    {
        private CinemachineVirtualCamera  vcam;

        private void Awake()
        {
            vcam = GetComponent<CinemachineVirtualCamera>();
        }
        
        private void Update()
        {
            vcam.Follow = GameObject.FindWithTag("FollowTarget").transform;
        }
    }
}
