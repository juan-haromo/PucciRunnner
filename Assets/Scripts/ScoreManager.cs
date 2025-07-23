using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    float score;
    public GameObject gamePanel;
    public TextMeshProUGUI scoreDisplay;
    public GameObject losePanel;
    public TextMeshProUGUI loseDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePanel.SetActive(true);
        loseDisplay.text = "Game over\nFinal score " + score;
        losePanel.SetActive(false);
        score = 0;
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * 3;
        scoreDisplay.text = ((int)score).ToString();
    }

    public void AddScore(float score)
    {
        this.score += score;
        scoreDisplay.text = ((int)score).ToString();
    }
    public void Lose()
    {
        gamePanel.SetActive(false);
        loseDisplay.text = "Game over\nFinal score " + (int)score;
        losePanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
