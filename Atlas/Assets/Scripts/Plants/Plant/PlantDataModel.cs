using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Variables;

namespace Plants
{
    [CreateAssetMenu(menuName = "Plant System/Plant Data")]
    public class PlantDataModel : ScriptableObject
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _scientific_name;
        [SerializeField]
        private float _max_height;
        [SerializeField]
        private string _ids_reproduction;
        [SerializeField]
        private string _ids_soil_type;
        [SerializeField]
        private string _ids_soil_ph;
        [SerializeField]
        private string _ids_soil_humidity;
        [SerializeField]
        private string _ids_sun_exposure;
        [SerializeField]
        private string _ids_soil_container;
        [SerializeField]
        private string _growth_rate;
        [SerializeField]
        private string _frozen_tolerance;
        [SerializeField]
        private string _planting_period;
        [SerializeField]
        private string _harvest_period;
        [SerializeField]
        private string _florering_period;
        [SerializeField]
        private string _cutting_period;
        [SerializeField]
        private float _growth_duration;

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }
        //  get { return float.Parse(_scientific_name, CultureInfo.InvariantCulture.NumberFormat);

        public string Scientifique_Name
        {
            get { return _scientific_name; }
        }

        public float Max_Height
        {
            get { return _max_height; }
        }

        public float Ids_reproduction
        {
            get
            {
                return float.Parse(_ids_reproduction, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Ids_soil_type
        {
            get
            {
                return float.Parse(_ids_soil_type, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Ids_soil_ph
        {
            get
            {
                return float.Parse(_ids_soil_ph, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Ids_soil_humidity
        {
            get
            {
                return float.Parse(_ids_soil_humidity, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Ids_sun_exposure
        {
            get
            {
                return float.Parse(_ids_sun_exposure, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Ids_soil_container
        {
            get
            {
                return float.Parse(_ids_soil_container, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Growth_rate
        {
            get
            {
                return float.Parse(_ids_soil_container, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Frozen_tolerance
        {
            get
            {
                return float.Parse(_frozen_tolerance, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public string Planting_period
        {
            get
            {
                return _planting_period;
            }
        }

        public string Harvest_period
        {
            get
            {
                return _harvest_period;
            }
        }

        public string Florering_period
        {
            get
            {
                return _florering_period;
            }
        }

        public string Cutting_period
        {
            get
            {
                return _cutting_period;
            }
        }

        public float Growth_duration
        {
            get { return _growth_duration; }
        }
    }
}
