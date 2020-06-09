using UnityEngine.UI;
using UnityEngine;
using SceneManagement;

public class SkipCinematic : MonoBehaviour
{
    private Image _spacePressed = null;
    public Image SpacePressed { get => _spacePressed; set => _spacePressed = value; }


    private Canvas _spaceLoading = null;
    public Canvas SpaceLoading
    {
        get => _spaceLoading;
        set
        {
            if (value != null)
                _spaceLoading = value;
        }
    }

    [Range(1, 5)]
    public int timeToPress = 3;

    private float currentTimePressing = 0.0f;

    private void Awake()
    {
        SpaceLoading = transform.Find("CinematicHUD").Find("Canvas").gameObject.transform.Find("SpaceLoading Canvas").gameObject.GetComponent<Canvas>();
        SpacePressed = SpaceLoading.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        SpaceLoading.enabled = false;
        SpacePressed.fillAmount = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartUpdate();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopUpdate();
        }
    }

    public void StartUpdate()
    {
        SpaceLoading.enabled = true;
        InvokeRepeating("UpdateLoadingFillAmount", 0.1f, 0.1f);
    }

    public void StopUpdate()
    {
        SpaceLoading.enabled = false;
        SpacePressed.fillAmount = 0.0f;
        currentTimePressing = 0.0f;
        CancelInvoke("UpdateLoadingFillAmount");
    }

    void UpdateLoadingFillAmount()
    {
        currentTimePressing += 0.1f;
        if (SpacePressed.fillAmount < 1.0f)
            FillSpaceImage(currentTimePressing / timeToPress);
        else
            SceneLoader.Instance.LoadScene(1, 4);
    }

    void FillSpaceImage(float amount)
    {
        SpacePressed.fillAmount = amount;
    }
}
