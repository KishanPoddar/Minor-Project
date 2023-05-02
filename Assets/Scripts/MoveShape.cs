using UnityEngine;

public class MoveShape : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public static int score = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject == this.gameObject && this.gameObject.name == GameManager.selectedShape)
                {
                    Destroy(this.gameObject);
                    score++;
                }
            }
        }
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Destroy(this.gameObject);
        }
    }

}
