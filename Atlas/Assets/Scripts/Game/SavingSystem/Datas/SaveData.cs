using System;
using Game.DayNight;
using UnityEngine;

namespace Game.SavingSystem.Datas
{
    public interface ISaveData<TStruct>
    {
        void Load(ref TStruct date);
        void Save(TStruct date);
    }
    
    [Serializable]
    public class DateData : ISaveData<Date>
    {
        public int Hours;
        public int Minutes;
        public float Seconds;
        public int Day;
        public int Month;    
        public int Year;

        public DateData(Date date)
        {
            Save(date);
        }

        public void Save(Date date)
        {
            Hours = date.Hours;
            Minutes = date.Minutes;
            Seconds = date.Seconds;
            
            Day = date.Day;
            Month = date.Month;
            Year = date.Year;
        }
        
        public void Load(ref Date date)
        {
            date.Hours = Hours;
            date.Minutes = Minutes;
            date.Seconds = Seconds;
            
            date.Day = Day;
            date.Month = Month;
            date.Year = Year;
        }
    }
    
    [Serializable]
    public struct TransformSaveData
    {
        public SaveVector       Position;
        public SaveQuaternion   Rotation;

        public TransformSaveData(Transform transform)
        {
            Position = new SaveVector(transform.position);
            Rotation = new SaveQuaternion(transform.rotation);
        }
            
        public void SetFromTransform(Transform transform)
        {
            Position = new SaveVector(transform.position);
            Rotation = new SaveQuaternion(transform.rotation);
        }
    }

    [Serializable]
    public struct SaveQuaternion
    {
        public float w;
        public float x;
        public float y;
        public float z;
            
        public Quaternion Value => (new Quaternion(x, y, z, w));

        public SaveQuaternion(Quaternion quaternion)
        {
            w = quaternion.w;
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
        }
    }

    [Serializable]
    public struct SaveVector
    {
        public float x;
        public float y;
        public float z;

        public Vector3 Value => (new Vector3(x, y, z));

        public SaveVector(Vector3 value)
        {
            x = value.x;
            y = value.y;
            z = value.z;
        }
    }
}
