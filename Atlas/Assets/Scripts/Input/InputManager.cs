using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        #region keys
        public static string FORWARD = "Forward";
        public static string BACKWARD = "Backward";
        public static string LEFT = "Left";
        public static string RIGHT = "Right";
        public static string LOOK_UP = "Lookup";
        public static string LOOK_DOWN = "Lookdown";
        public static string LOOK_RIGHT = "LookRight";
        public static string LOOK_LEFT = "LoolLeft";
        public static string JUMP = "Jump";
        public static string SPRINT = "Sprint";
        public static string CROUCH = "Crouch";
        public static string PRONE = "Prone";
        public static string USE = "Use";
        public static string INVENTORY = "Inventory";
        public static string PICK = "Pick";
        public static string AXIS_HORIZONTAL = "Horizontal";
        public static string AXIS_VERTICAL = "Vertical";
        public static string R_AXIS_HORIZONTAL = "RHorizontal";
        public static string R_AXIS_VERTICAL = "RVertical";
        public static string CAMERA_LOCK = "CameraLock";
        public static string CAMERA_ZOOM_IN = "CameraZoomIn";
        public static string CAMERA_ZOOM_OUT = "CameraZoomOut";
        public static string AXIS_CAMERA_ZOOM = "CameraZoom";
        #endregion

        private void Awake()
        {
            cInput.Init();
            //cInput.Clear();
        }

        void Start()
        {
            bool shouldSetJoystick = IsJoystickConnected();

            #region Movement
            cInput.SetKey(FORWARD, Keys.W, Keys.Z);
            cInput.SetKey(BACKWARD, Keys.S);
            cInput.SetKey(LEFT, Keys.A, Keys.Q);
            cInput.SetKey(RIGHT, Keys.D);
            cInput.SetKey(JUMP, Keys.Space);
            cInput.SetKey(SPRINT, Keys.LeftShift);
            cInput.SetKey(CROUCH, Keys.C);
            cInput.SetKey(PRONE, Keys.V);
            cInput.SetAxis(AXIS_HORIZONTAL, LEFT, RIGHT);
            cInput.SetAxis(AXIS_VERTICAL, BACKWARD, FORWARD);
            if (shouldSetJoystick)
            {
                cInput.SetKey(JUMP, Keys.Joystick1Button0);
                cInput.SetKey(SPRINT, Keys.Joystick1Button1);
                cInput.SetKey(CROUCH, Keys.Joystick1Button4);
                cInput.SetKey(PRONE, Keys.Joystick1Button5);
            }
            #endregion

            #region Look
            cInput.SetKey(LOOK_UP, Keys.MouseUp);
            cInput.SetKey(LOOK_DOWN, Keys.MouseDown);
            cInput.SetKey(LOOK_LEFT, Keys.MouseLeft);
            cInput.SetKey(LOOK_RIGHT, Keys.MouseRight);
            cInput.SetKey(CAMERA_LOCK, Keys.Mouse1);
            cInput.SetKey(CAMERA_ZOOM_IN, Keys.MouseWheelUp);
            cInput.SetKey(CAMERA_ZOOM_OUT, Keys.MouseWheelDown);
            cInput.SetAxis(R_AXIS_HORIZONTAL, LOOK_LEFT, LOOK_RIGHT);
            cInput.SetAxis(R_AXIS_VERTICAL, LOOK_DOWN, LOOK_UP);
            cInput.SetAxis(AXIS_CAMERA_ZOOM, CAMERA_ZOOM_OUT, CAMERA_ZOOM_IN);

            // TODO : make zoom option for gamepad... or not
            #endregion

            #region Actions
            cInput.SetKey(USE, Keys.E);
            cInput.SetKey(INVENTORY, Keys.I);
            cInput.SetKey(PICK, Keys.E);
            if (shouldSetJoystick)
            {
                cInput.SetKey(USE, Keys.Joystick1Button2);
                cInput.SetKey(INVENTORY, Keys.Joystick1Button3);
                //TODO MAN: Find a joystick key for pick
            }
            #endregion

        }

        static public bool IsJoystickConnected()
        {
            return Input.GetJoystickNames().Length > 0;
        }
    }
}