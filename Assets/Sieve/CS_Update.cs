using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Update : MonoBehaviour
{
    [SerializeField] private GameObject Scrollbox;
    [SerializeField] private GameObject ResultTemplate;
    [SerializeField] private SO_Sieve SieveToRun;

    private int BatchCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SieveToRun.init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunSieve()
    {
        BatchCount++;
        DateTime tStart = DateTime.UtcNow;
        int passes = 0;
        while ((DateTime.UtcNow - tStart).TotalMilliseconds <= SieveToRun.getRunLengthMS())
        {
            SieveToRun.clearBitarray();
            SieveToRun.runSieve();
            passes++;
        }
        TimeSpan tD = DateTime.UtcNow - tStart;
        Debug.Log(SieveToRun.validateResults());
        GameObject g;
        g = Instantiate(ResultTemplate, Scrollbox.transform);
        g.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = BatchCount.ToString();
        g.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = SieveToRun.sieveSize.ToString();
        g.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = SieveToRun.getRunLengthMS().ToString();
        g.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = tD.TotalMilliseconds.ToString();
        g.transform.GetChild(4).GetComponent<UnityEngine.UI.Text>().text = passes.ToString();
    }

    public void ClearResults()
    {

        BatchCount = 0;

        int childs = Scrollbox.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(Scrollbox.transform.GetChild(i).gameObject);
        }
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
