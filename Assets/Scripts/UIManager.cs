using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject coinCountDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI (int newCoinCount)
    {       
        coinCountDisplay.GetComponent<TextMeshProUGUI>().text = "x" + newCoinCount;
    }
}
