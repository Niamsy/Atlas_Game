using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public class Stage : ScriptableObject
    {
        public struct Need
        {

        }

        private List<Soil>          _soils;
        private List<Need>          _needs;
        private Material            _model;
        private AtlasAudio.Audio    _audio;
        private float               _height;
        private float               _temperature;

        public List<Soil> Soils
        {
            get { return _soils; }
        }

        public List<Need> Needs
        {
            get { return _needs; }
        }

        public Material Model
        {
            get { return _model; }
        }

        public AtlasAudio.Audio Audio
        {
            get { return _audio; }
        }

        public float Height
        {
            get { return _height; }
        }

        public float Temperature
        {
            get { return _temperature; }
        }
    }
}
