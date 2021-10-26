using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public float panSpeed = 50f;
    public float panBorder = 10f;
    public float scrollSpeed = 5f;

    [Header("Clamps")]
    private float minX = -0f;
    private float maxX = 25f;
    private float minY = 10f;
    private float maxY = 80f;
    private float minZ = -10f;
    private float maxZ = 10f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder)
        {
            if (transform.position.z <= maxZ)
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorder)
        {
            if (transform.position.z >= minZ)
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder)
        {
            if (transform.position.x <= maxX)
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorder)
        {
            if (transform.position.x >= minX)
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

    }
}
