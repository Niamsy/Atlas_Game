using Game.DayNight;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class Minimap : MonoBehaviour
    {
        private Color WinterColor = new Color32(64, 89, 128, 255);
        private Color SpringColor = new Color32(100, 125, 95, 255);
        private Color SummerColor = new Color32(167, 82, 99, 255);
        private Color AutumnColor = new Color32(168, 128, 59, 255);

        private string[] monthText =
        {
            "JAN",
            "FEB",
            "MAR",
            "APR",
            "MAY",
            "JUN",
            "JUL",
            "AUGT",
            "SEP",
            "OCT",
            "NOV",
            "DEC"
        };
    
        // Start is called before the first frame update
        void Start()
        {
        }
    }
}