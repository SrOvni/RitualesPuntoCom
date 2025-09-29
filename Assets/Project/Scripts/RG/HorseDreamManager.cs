using System.Collections;
using ImprovedTimers;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HorseDreamManager : Singleton<HorseDreamManager>
{
    public MetalHorse metalHorse;
    [SerializeField] private float horseDreamDuration = 10;
    CountdownTimer horseDreamTimer;
    Slider progressSlider;
    [SerializeField] CinemachineCamera horseCamera;
    [SerializeField] private AudioData MetalHorseAudio;
    GameObject metalHorseGameobject;

    private void Start()
    {
        horseDreamTimer = new(horseDreamDuration);
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // 2. Crear Slider
        GameObject sliderGO = new GameObject("ProgressBar");
        sliderGO.transform.SetParent(canvasGO.transform);
        progressSlider = sliderGO.AddComponent<Slider>();

        // 3. Ajustar RectTransform
        RectTransform rt = sliderGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 40);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;

        // 4. Crear Background e Fill Area (requerido por el Slider)
        GameObject background = new GameObject("Background");
        background.transform.SetParent(sliderGO.transform);
        Image bgImage = background.AddComponent<Image>();
        bgImage.color = Color.gray;
        RectTransform bgRT = background.GetComponent<RectTransform>();
        bgRT.anchorMin = Vector2.zero;
        bgRT.anchorMax = Vector2.one;
        bgRT.offsetMin = Vector2.zero;
        bgRT.offsetMax = Vector2.zero;

        GameObject fillArea = new GameObject("Fill");
        fillArea.transform.SetParent(sliderGO.transform);
        Image fillImage = fillArea.AddComponent<Image>();
        fillImage.color = Color.green;
        RectTransform fillRT = fillArea.GetComponent<RectTransform>();
        fillRT.anchorMin = Vector2.zero;
        fillRT.anchorMax = Vector2.one;
        fillRT.offsetMin = Vector2.zero;
        fillRT.offsetMax = Vector2.zero;

        progressSlider.fillRect = fillRT;
        progressSlider.targetGraphic = bgImage;
        progressSlider.minValue = 0f;
        progressSlider.maxValue = 1f;
        progressSlider.gameObject.SetActive(false);
    }
    public IEnumerator HorseDream()
    {
        horseCamera.Prioritize();
        yield return new WaitForSeconds(Camera.main.GetComponent<CinemachineBrain>().ActiveBlend.Duration);
        progressSlider.gameObject.SetActive(true);
        horseDreamTimer.Start();
        AudioPlayer.Instance.Play(MetalHorseAudio);
        yield return new WaitUntil(() => !horseDreamTimer.IsRunning);
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        if (horseDreamTimer.IsRunning)
        {
            progressSlider.value = horseDreamTimer.Progress;
        }
    }
}
