using System;
using Cinemachine;
using Game;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CinemachineFreeLookInputConversion : MonoBehaviour
{
    public CursorLockMode lockCursor;
    public bool lockCamera;
    public bool rotateCameraOnClick;

    private CinemachineFreeLook m_freeLookCamera;
    private Vector2 m_MouseDelta;
    private Vector2 m_Scroll;
    private bool m_CameraClick;

    private void Awake()
    {
        m_freeLookCamera = GetComponent<CinemachineFreeLook>();
        CinemachineCore.GetInputAxis = GetAxisCustom;
        Cursor.lockState = lockCursor;
    }

    private void OnEnable()
    {
        GameControl.Instance.InputControls.Player.CameraMovement.performed += ctx => CameraMovement(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraMovement.canceled += ctx => CancelCameraMovement(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraMovement.Enable();

        //BUG ? new input system trigger scroll wheel Input when mouse move
        GameControl.Instance.InputControls.Player.CameraZoom.performed += ctx => CameraZoom(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraZoom.canceled += ctx => CancelCameraZoom(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraZoom.Enable();

        GameControl.Instance.InputControls.Player.CameraClick.performed += ctx => CameraClick();
        GameControl.Instance.InputControls.Player.CameraClick.canceled += ctx => CancelCameraClick();
        GameControl.Instance.InputControls.Player.CameraClick.Enable();

        GameControl.Instance.InputControls.Player.RecenterCamera.performed += ctx => RecenterCamera();
        GameControl.Instance.InputControls.Player.RecenterCamera.Enable();
    }

    private void OnDisable()
    {
        GameControl.Instance.InputControls.Player.CameraMovement.performed -= ctx => CameraMovement(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraMovement.canceled -= ctx => CancelCameraMovement(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraMovement.Disable();
        GameControl.Instance.InputControls.Player.CameraZoom.performed -= ctx => CameraZoom(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraZoom.canceled -= ctx => CancelCameraZoom(ctx.ReadValue<Vector2>());
        GameControl.Instance.InputControls.Player.CameraZoom.Disable();
        GameControl.Instance.InputControls.Player.CameraClick.performed -= ctx => CameraClick();
        GameControl.Instance.InputControls.Player.CameraClick.canceled -= ctx => CancelCameraClick();
        GameControl.Instance.InputControls.Player.CameraClick.Disable();
        GameControl.Instance.InputControls.Player.RecenterCamera.performed -= ctx => RecenterCamera();
        GameControl.Instance.InputControls.Player.RecenterCamera.Disable();
    }

    private float GetAxisCustom(string axisName)
    {
        if (lockCamera)
            return 0;
        if (((rotateCameraOnClick && m_CameraClick) || !rotateCameraOnClick) && axisName == "Mouse X Remaped")
            return m_MouseDelta.x;
        else if (((rotateCameraOnClick && m_CameraClick) || !rotateCameraOnClick) && axisName == "Mouse Y Remaped")
            return m_MouseDelta.y;
        else if (axisName == "Mouse ScrollWheel Remaped")
            return m_Scroll.y;
        return 0;
    }

    private void CameraMovement(Vector2 mouseDelta)
    {
        m_MouseDelta = mouseDelta;
    }

    private void CancelCameraMovement(Vector2 vector2)
    {
        m_MouseDelta = Vector2.zero;
    }

    private void CameraZoom(Vector2 scroll)
    {
        m_Scroll = scroll;
    }

    private void CancelCameraZoom(Vector2 vector2)
    {
        m_Scroll = Vector2.zero;
    }

    private void CameraClick()
    {
        m_CameraClick = true;
    }

    private void CancelCameraClick()
    {
        m_CameraClick = false;
    }

    private void RecenterCamera()
    {
        // not working as for now, bug from unity, will be fixed in futur cinemachine release
        m_freeLookCamera.m_RecenterToTargetHeading.RecenterNow();
    }
}
