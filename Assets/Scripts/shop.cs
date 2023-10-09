using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    // wellness, money, reputation

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buyItem()
    {
        //++ wellness
        //-- money
        // ++ reputation
        // block future purchse
    }
}

public class buyableItem
{
    private int wellnessChange;
    private int reputationChange;
    private double moneyChange;
    private bool purchased;

    public buyableItem(int w, int r, double m)
    {
        wellnessChange = w;
        reputationChange = r;
        moneyChange = m;
        purchased = false;
    }


}
