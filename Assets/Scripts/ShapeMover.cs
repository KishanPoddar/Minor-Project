using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShapeMover : MonoBehaviour
{
    private string[] ProtoNames = {
        "Cube",
        "Cylinder",
        "Sphere",
        "Cuboid"};

    private GameManager gameManager;
    private string currentShapeName;
    public static int poleIndex = 0;
    static float direction;

    [SerializeField] private GameObject[] shapes;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private GameObject SelectionPoles;
    [SerializeField] private Transform []poleSpawnPoints;
    [SerializeField] private GameObject playEnvironment;
    [SerializeField] private List<GameObject> poleList;

    Camera cam;

    void Start()
    {
        poleList = new List<GameObject>();
        gameManager = FindObjectOfType<GameManager>();
        gameObject.name = ProtoNames[0];
        cam = Camera.main;
        poleList.Add(Instantiate(SelectionPoles, poleSpawnPoints[poleIndex].position, SelectionPoles.transform.rotation));
        StartCoroutine(SpawnPoles());
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.5f, 3.5f), transform.position.y, transform.position.z);

        if (playEnvironment.activeInHierarchy)
        {
            cam.transform.position = new Vector3(transform.position.x, 2, transform.position.z - 10);
            transform.Translate(cam.transform.forward * moveSpeed * Time.deltaTime);
            transform.Translate(cam.transform.right * direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pole"))
        {
            other.GetComponent<Collider>().enabled = false;
            StartCoroutine(ChangeShape());
            

            if(transform.name != other.gameObject.GetComponentInChildren<Text>().text.ToString())
            {
                Time.timeScale = 0;
                Debug.Log(transform.name + other.gameObject.GetComponentInChildren<Text>().text);
                gameManager.OpenGameOverPanel();
                MoveShape.score = 0;
                foreach (GameObject pole in poleList)
                {
                    Destroy(pole);
                }
                poleList.Clear();
            }
            else
            {
                MoveShape.score++;
            }

           

        }
    }

    IEnumerator ChangeShape()
    {
        int selectedIndex = Random.Range(0, shapes.Length);
        GameObject randomShape = shapes[selectedIndex];
        transform.localScale =  shapes[selectedIndex].transform.localScale;


        poleIndex++;
        GameObject spawnedPole = Instantiate(SelectionPoles, poleSpawnPoints[poleIndex].position, SelectionPoles.transform.rotation);
        poleList.Add(spawnedPole);
        int randomIndex = Random.Range(0, ProtoNames.Length);
       /* spawnedPole.transform.GetChild(3).GetChild(0).GetComponentInChildren<Text>().text = ProtoNames[randomIndex];
       */


        yield return new WaitForSeconds(.5f);

        int randomSide = Random.Range(0, 2);

        Debug.Log(ProtoNames[selectedIndex] + randomSide);
        gameObject.GetComponent<MeshFilter>().mesh = randomShape.gameObject.GetComponent<MeshFilter>().mesh;
        gameObject.name = ProtoNames[selectedIndex];


        if (selectedIndex < ProtoNames.Length - 1)
        {
            if (randomSide == 0)
            { 
                spawnedPole.transform.GetChild(3).GetChild(randomSide).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex];
                spawnedPole.transform.GetChild(3).GetChild(randomSide + 1).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex + 1];
            }
            else
            {
                spawnedPole.transform.GetChild(3).GetChild(randomSide).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex];
                spawnedPole.transform.GetChild(3).GetChild(randomSide - 1).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex + 1];
            }
                
        }
        else
        {
            if (randomSide == 0)
            {
                spawnedPole.transform.GetChild(3).GetChild(randomSide).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex];
                spawnedPole.transform.GetChild(3).GetChild(randomSide + 1).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex - 1];
            }
            else
            {
                spawnedPole.transform.GetChild(3).GetChild(randomSide).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex];
                spawnedPole.transform.GetChild(3).GetChild(randomSide - 1).GetComponentInChildren<Text>().text = ProtoNames[selectedIndex - 1];
            }
        }
    }

    IEnumerator SpawnPoles()
    {
        for(int i = 0; i < 10; i++)
        {
            
            yield return new WaitForSeconds(1f);
           
        }
    }

    public void Move(int index)
    {
        direction = index;
    }
}
