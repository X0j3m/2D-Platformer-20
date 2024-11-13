using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float speed = 0.1f;
    public const int PLATFORMS_NUM = 4;
    public const float radius = 4;
    private GameObject[] platforms;
    private Vector2[] positions;
    private Vector3[] DstPositions;
    private void Awake()
    {
        platforms = new GameObject[PLATFORMS_NUM];
        positions = new Vector2[PLATFORMS_NUM];
        for (int i = 0; i < platforms.Length; i++)
        {
            positions[i].x = Mathf.Sin(i * Mathf.PI /2) * radius;
            positions[i].y = Mathf.Cos(i * Mathf.PI/2) * radius;
            platforms[i] = Instantiate(platformPrefab, positions[i], Quaternion.identity);
           // DstPositions[i] = positions[i];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            //platforms[i].transform.position = Vector3.MoveTowards(platforms[i].transform.position, DstPositions[(i+1)%PLATFORMS_NUM], speed * Time.deltaTime);
        }
    }
}
