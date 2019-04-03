﻿using UnityEngine;

namespace Atlas_Physics
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AtlasGravity))]
    public class BodyController : MonoBehaviour
    { 
        #region public variables
        #endregion

        #region properties
        public bool IsGrounded { get; protected set; }
        public bool IsCeilinged { get; protected set; }
        public Vector3 Velocity { get; protected set; }
        public Rigidbody Rigidbody { get { return _Rigidbody; } }
        #endregion

        #region private variables
        Rigidbody _Rigidbody;
        Vector3 _PreviousPosition;
        Vector3 _CurrentPosition;
        Vector3 _NextMovement;
        #endregion

        void Awake()
        {
            _Rigidbody = GetComponent<Rigidbody>();
            _CurrentPosition = _Rigidbody.position;
            _PreviousPosition = _Rigidbody.position;
        }

        void FixedUpdate()
        {
            _PreviousPosition = _Rigidbody.position;
            _CurrentPosition = _PreviousPosition + _NextMovement;
            Velocity = (_CurrentPosition - _PreviousPosition) / Time.fixedDeltaTime;
            _Rigidbody.MovePosition(_CurrentPosition);
            _NextMovement = Vector3.zero;
        }

        /// <summary>
        /// This moves a rigidbody and so should only be called from FixedUpdate or other Physics messages.
        /// </summary>
        /// <param name="movement">The amount moved in global coordinates relative to the rigidbody2D's position.</param>
        public void Move(Vector3 movement)
        {
            _NextMovement += movement;
        }

        /// <summary>
        /// This moves the character without any implied velocity.
        /// </summary>
        /// <param name="position">The new position of the character in global space.</param>
        public void Teleport(Vector3 position)
        {
            Vector3 delta = position - _CurrentPosition;
            _PreviousPosition += delta;
            _CurrentPosition = position;
            _Rigidbody.MovePosition(position);
        }
    }
}