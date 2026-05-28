using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 45f;
    private GameObject m_spawnedCube;

    void Start()
    {
        m_spawnedCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        m_spawnedCube.name = "RotatingCube";
        m_spawnedCube.transform.position = Vector3.zero;
    }

    void Update()
    {
        if (m_spawnedCube != null)
        {
            m_spawnedCube.transform.Rotate(m_rotationSpeed * Time.deltaTime, m_rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
