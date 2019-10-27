﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

namespace Game.Questing
{
    public class AData : ScriptableObject
    {
        protected void OnEnable()
        {
            if (Id.Empty())
            {
                Id = GUID.Generate();
            }
        }

        public GUID Id { get; private set; }

        #region Equality Operators
        protected bool Equals(AData other)
        {
            return base.Equals(other) && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ Id.GetHashCode();
            }
        }

        public static bool operator ==(AData data1, AData data2)
        {
            return data1.Id == data2.Id;
        }

        public static bool operator !=(AData data1, AData data2)
        {
            return !(data1 == data2);
        }
        #endregion
    }
}