using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField] private float scrollSpeed = 0.5f;
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        material.mainTextureOffset += new Vector2(0, scrollSpeed) * Time.deltaTime/ 10;
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }
}
