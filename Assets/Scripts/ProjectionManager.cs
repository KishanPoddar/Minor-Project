using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProjectionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    public static string shapeName;

    void Start()
    {
        ClosePanels();
    }


    public void OpenScenes(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangePanel(int index)
    {
        ClosePanels();
        panels[index].SetActive(true);
    }

    public void ClosePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void GoBack()
    {
        OpenScenes(0);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ClosePanels();
            panels[ShapeSelector.shapeIndex].SetActive(true);
        }
    }
}
