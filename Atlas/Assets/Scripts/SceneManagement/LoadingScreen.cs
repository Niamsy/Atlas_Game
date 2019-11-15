using Menu;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("Tips")]
        public Text tipText;
        private SceneLoader _sceneLoader = null;

        private void Awake()
        {
            _sceneLoader = SceneLoader.Instance;
            tipText.text = GetRandomTip();
        }

        private string GetRandomTip()
        {
            string tip = "";

            string filePath = Application.dataPath + "/Resources/Tips/data.json";
            Debug.Log(filePath);
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                Tip[] tips = JsonHelper.FromJson<Tip>(dataAsJson);
                int random = Random.Range(0, 5);
                tip = tips[random].GetLoc("ENG");
            }
            else
            {
                Debug.LogError("Cannot load game data!");
            }
            return tip;
        }
    }
}
