using System;
using System.Collections;
using System.Collections.Generic;
using Leon;
using PlayerActions;
using UnityEngine;

public class CameraController : SceneSingleton<CameraController>
{
    private List<VirtualCamera> _virtualCameraList = new List<VirtualCamera>();

    private void OnEnable()
    {
        SitAction.Instance.OnSit += SitAction_OnSit;
        SitAction.Instance.OnStand += SitAction_OnStand;
    }

    private void OnDisable()
    {
        SitAction.Instance.OnSit -= SitAction_OnSit;
        SitAction.Instance.OnStand -= SitAction_OnStand;
    }

    private void SitAction_OnSit() => _virtualCameraList.ForEach(x => x.Toggle(VirtualCamera.CameraType.FPS));
    private void SitAction_OnStand() => _virtualCameraList.ForEach(x => x.Toggle(VirtualCamera.CameraType.TPS));

    public void AddToVirtualCameraList(VirtualCamera virtualCamera) => _virtualCameraList.Add(virtualCamera);
}