using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Texture2D DefaultCursor;
    public Texture2D HoverCursor;

    [SerializeField] private GameObject _endingCanvas;
    [SerializeField] private TextMeshProUGUI _endingName;

    private EndingData _currentEndingData;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void RestartCurrentScene()
    {
        Debug.Log("a");
        SceneManager.LoadScene(0);
    }

    public void SetCurrentEndingData(EndingData endingData)
    {
        _currentEndingData = endingData;
    }
    public void ShowEndingBoard()
    {
        _endingCanvas.SetActive(true);
        _endingName.text = _currentEndingData.EndingName;
    }
}
