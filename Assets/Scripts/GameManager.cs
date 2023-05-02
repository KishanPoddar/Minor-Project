using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject BGPanel;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject LevelFinishedPanel;
    [SerializeField] private GameObject scoreTxtObj;
    [SerializeField] private GameObject playEnvironment;
    [SerializeField] private GameObject buttonPanel;


    private string[] Shapes = {"Cube", "Sphere", "Cuboid", "Cylinder"};
    public static string selectedShape;
    private Text shapeSelectionTxt;
    private static bool isRestarted = false;

    void Start()
    {
        if (!isRestarted)
        {
            CloseMenus();
            BGPanel.SetActive(true);
        }
        else
        {
            CloseMenus();
            playEnvironment.SetActive(true);
        }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                shapeSelectionTxt = GameObject.Find("ShapeSelectionTxt").GetComponent<Text>();
                selectedShape = Shapes[Random.Range(0, Shapes.Length)];
                shapeSelectionTxt.text = selectedShape;
            }
    }


    void Update()
    {
        if (playEnvironment.gameObject.activeInHierarchy)
        {
            scoreTxtObj.gameObject.SetActive(true);
            scoreTxtObj.GetComponent<Text>().text = "Score : " + MoveShape.score;
            if (MoveShape.score >= 10)
            {
                OpenCompletionPanel();
                Time.timeScale = 0;
            }
        }
        else
            scoreTxtObj.gameObject.SetActive(false);

    }

    public void OpenLevels (int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void CloseMenus() 
    {
        BGPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        LevelFinishedPanel.SetActive(false);
        buttonPanel.SetActive(false);
    }

    public void OpenGameOverPanel()
    {
        ShapeMover.poleIndex = 0;
        CloseMenus();
        GameOverPanel.SetActive(true);
    }

    public void OpenCompletionPanel()
    {
        CloseMenus();
        LevelFinishedPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        CloseMenus();
        playEnvironment.SetActive(true);
        buttonPanel.SetActive(true);
    }
    public void RetryForLevel1()
    {
        isRestarted = true;
        OpenLevels(2);
        Time.timeScale = 1;
        CloseMenus();
        playEnvironment.SetActive(true);
    }
}
