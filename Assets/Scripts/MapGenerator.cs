using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Size")]
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;

    [Header("Prefabs")]
    [SerializeField] Wall[] wallPrefabs;
    [SerializeField] Monster[] monsterPrefabs;

    [Header("Rooms")]
    [SerializeField] int roomCount;
    [SerializeField] ValueTuple<int, int>[] centerPivots;
    [SerializeField] ValueTuple<int, int>[] centers;

    [SerializeField] GameObject walls;
    [SerializeField] GameObject monsters;

    private void Awake()
    {
        centers = new ValueTuple<int, int>[] {
            new ValueTuple<int, int>(width/4, height/4),
            new ValueTuple<int, int>(width/4, 3*height/4),
            new ValueTuple<int, int>(3*width/4, height/4),
            new ValueTuple<int, int>(3*width/4, 3*height/4)
        };

        InitMap();

        for (int i = 0; i < roomCount; i++)
        {
            int size = UnityEngine.Random.Range(5, 8);
            Debug.Log($"[Room{i}] center: ({centers[i].Item1}, {centers[i].Item2}), size: {size}");

            GenerateRoom(centers[i], size);
            GenerateMonster(centers[i], size);
        }

        SetBorder();
    }

    private void Start()
    {
        // cellSize = GameManager.Instance.map.cellSize.x;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(centers[0].Item1, centers[0].Item2) * cellSize;
    }

    private void InitMap()
    {
        for (int i = 1; i < width - 1; i++)
            for (int j = 1; j < height - 1; j++)
            {
                int wallType = UnityEngine.Random.Range(0, wallPrefabs.Length);
                Wall newWall = Instantiate(wallPrefabs[wallType], new Vector3(i, j) * cellSize, Quaternion.identity);
                newWall.transform.SetParent(walls.transform);
            }
    }

    private void GenerateRoom(ValueTuple<int, int> center, int size)
    {
        size /= 2;
        for (int i = center.Item1 - size; i <= center.Item1 + size; i++)
            for (int j = center.Item2 - size; j <= center.Item2 + size; j++)
            {
                Collider2D collider = Physics2D.OverlapCircle(new Vector3(i, j) * cellSize, 0.2f);
                if (collider != null)
                    Destroy(collider.gameObject);
            }
    }

    private void GenerateMonster(ValueTuple<int, int> center, int size)
    {
        size /= 2;
        for (int k = 0; k < size; k++)
        {
            int monsterType = UnityEngine.Random.Range(0, monsterPrefabs.Length);
            int x = UnityEngine.Random.Range(center.Item1 - size, center.Item1 + size + 1);
            int y = UnityEngine.Random.Range(center.Item2 - size, center.Item2 + size + 1);

            Monster newMonster = Instantiate(monsterPrefabs[monsterType], new Vector3(x, y) * cellSize, Quaternion.identity);
            newMonster.transform.SetParent(monsters.transform);
        }
    }

    private void SetBorder()
    {
        Wall newWall;
        for (int i = 1; i < width - 1; i++)
        {
            newWall = Instantiate(wallPrefabs[2], new Vector3(i, 0) * cellSize, Quaternion.identity);
            newWall.transform.SetParent(walls.transform);
            newWall = Instantiate(wallPrefabs[2], new Vector3(i, height - 1) * cellSize, Quaternion.identity);
            newWall.transform.SetParent(walls.transform);
        }

        for (int j = 1; j < height - 1; j++)
        {
            newWall = Instantiate(wallPrefabs[2], new Vector3(0, j) * cellSize, Quaternion.identity);
            newWall.transform.SetParent(walls.transform);
            newWall = Instantiate(wallPrefabs[2], new Vector3(width - 1, j) * cellSize, Quaternion.identity);
            newWall.transform.SetParent(walls.transform);
        }
    }
}
