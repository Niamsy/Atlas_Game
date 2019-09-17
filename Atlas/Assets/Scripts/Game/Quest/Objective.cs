using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum ObjType
    {
        PICKUP,
        REACH,
        PLANT,
    };

    public class Objective
    {
        private ObjType type;
        private string description { get; }
        private string comparedId;
        private int countToFinal { get; set; }
        private int finalCount { get; }


        public Objective(ObjType t, string comp, int fc = 1) //fc == final count
        {
            type = t;
            comparedId = comp;
            finalCount = fc;
        }

        public ObjType getObjType()
        {
            return type;
        }
        
        public bool isComplete()
        {
            return countToFinal == finalCount;
        }

        public bool compare(string tested)
        {
            if (tested == comparedId)
            {
                if (countToFinal < finalCount)
                {
                    countToFinal++;
                }
                return true;
            }
            return false;
        }
    }
