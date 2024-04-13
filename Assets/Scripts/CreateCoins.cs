using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoins : MonoBehaviour
{
    public GameObject coinPrefab;

    private List<GameObject> allCoins = new List<GameObject>();

    public int createCoinsNum = 8; // 生成金币的数量

    private HashSet<int> selectedIndices = new HashSet<int>();  // 声明一个HashSet<int>对象，用于存储已经选择的元素的索引

    // Start is called before the first frame update
    private void Start()
    {
        GetAllCoins();
        SelectRandomItems();
    }

    private void GetAllCoins()
    {
        foreach (Transform coin in transform)
        {
            allCoins.Add(coin.gameObject);
        }
    }

    private void SelectRandomItems()
    {
        int numObjects = allCoins.Count;

        for (int i = 0; i < createCoinsNum; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, numObjects);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);

            GameObject selectedObject = allCoins[randomIndex];

            //Debug.Log("Selected object: " + selectedObject.name);

            GameObject newCoin = Instantiate(coinPrefab, transform);
            newCoin.transform.position = selectedObject.transform.position;
        }
    }
}
