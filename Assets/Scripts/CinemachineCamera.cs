using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CinemachineCamera : MonoBehaviour
{
    CinemachineVirtualCamera _CinemachineVirtualCamera;
    CinemachineConfiner _CinemachineConfiner;

    void Start()
    {
        _CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _CinemachineConfiner = GetComponent<CinemachineConfiner>();

        SetUpScene();
    }

    void SetUpScene()
    {
        _CinemachineVirtualCamera.Follow = GameObject.Find("Player").transform;
        _CinemachineVirtualCamera.LookAt = GameObject.Find("Player").transform;

        _CinemachineConfiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("ScreenBounds").GetComponent<PolygonCollider2D>();
    }
}
