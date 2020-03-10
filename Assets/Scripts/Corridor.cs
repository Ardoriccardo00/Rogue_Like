using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    [Header("Path Creator")]
    [SerializeField] int minSquares = 5;
    [SerializeField] int maxSquares = 15;
    [SerializeField] int squaresToGenerate;

    [Header("Components")]
    Vector2 oldPos;
    [SerializeField] GameObject squarePrefab;
    [SerializeField] GameObject corridorParent;
    public List<GameObject> squareList = new List<GameObject>();
    public GameObject firstSquare;
    public GameObject lastSquare;

    public void GenerateCorridor()
    {
        float randX = Random.Range(0, GenerateWorld.instance.worldSize.x);
        float randY = Random.Range(0, GenerateWorld.instance.worldSize.y);

        oldPos = new Vector2(randX, randY);
        squaresToGenerate = Random.Range(minSquares, maxSquares);
        int direction = Random.Range(1, 4); //up, down, left, right

        GameObject squareParent = Instantiate(corridorParent, Vector2.zero, Quaternion.identity);
        for(int i = 0; i < squaresToGenerate; i++)
        {
            switch(direction)
            {
                case 1:
                    oldPos.x += 0;
                    oldPos.y += 1;
                    break;
                case 2:
                    oldPos.x += 0;
                    oldPos.y -= 1;
                    break;
                case 3:
                    oldPos.x -= 1;
                    oldPos.y += 0;
                    break;
                case 4:
                    oldPos.x += 1;
                    oldPos.y += 0;
                    break;
            }
            var newSquare = Instantiate(squarePrefab, oldPos, Quaternion.identity);
            newSquare.transform.SetParent(squareParent.transform);
            squareList.Add(newSquare);
        }
        firstSquare = squareList[0];
        lastSquare = squareList[squareList.Count - 1];
    }
}
