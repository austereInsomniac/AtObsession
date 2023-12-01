using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mackenzie 

// this script needs to be palced on the plyer object to work properly

public class buyableItem
{
    private int wellnessChange;
    private int reputationChange;
    private double moneyChange;
    private string name;
    private bool sprite;
    private bool purchased;
    private bool multiple;

    public buyableItem(int w, int r, double m, string n, bool s, bool mm)
    {
        wellnessChange = w;
        reputationChange = r;
        moneyChange = m;
        name = n;
        sprite = s;
        purchased = false;
        multiple = mm;
    }

    public void purchaseItem()
    {
        if (multiple || !purchased)
        {
            purchased = true;
        }
    }

    public int getWellness() {  return wellnessChange; }
    public int getReputation() { return reputationChange; }
    public double getMoney() {  return moneyChange; }
    public string getName() { return name; }
    public bool hasSprite() { return sprite; }
    public bool isPurchased() {  return purchased; }
}

public class shop : MonoBehaviour
{
    List<buyableItem> buyableItems;

    // which item to buy
    [SerializeField]
    private int itemIndex;

    private bool wasBought = false;

    // Start is called before the first frame update
    void Start()
    {
        // propogate the list of purchaseble items
        buyableItems = new List<buyableItem>
        {
            new buyableItem(10, 5, -25, "Headphones", true, false),
            new buyableItem(20, 5, -80, "Chair", true, false),
            new buyableItem(10, 5, -50, "Microphone", true, false),
            new buyableItem(15, 7, -75, "Monitor", true, false),
            new buyableItem(10, 5, -30, "New Outfit", false, true),
        };
    }
    
    // this is the method called by the button.
    // the int input is the index of the arry representing which item to buy
    public void buyItem(int index)
    {
        // get buyableItem
        buyableItem item = buyableItems[index];

        //check if the item had been purchased
        if (GetComponent<game_state>().getMoney() >= (-1 * item.getMoney()))
        {
            // adjust the game state data
            GetComponent<game_state>().updateWellness(item.getWellness());
            GetComponent<game_state>().updateMoney(item.getMoney());
            GetComponent<game_state>().updateReputation(item.getReputation());

            // block future purchse
            item.purchaseItem();
            wasBought = true;
        }

        if (item.hasSprite())
        {
            GameObject alternate = GameObject.Find(item.getName() + "2");
            GameObject.Find(item.getName()).SetActive(false);
            alternate.GetComponent<SpriteRenderer>().enabled = true;
            if(alternate.GetComponent<BoxCollider2D>() != null)
            {
                alternate.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public buyableItem getItem(int index)
    {
        return buyableItems[index];
    }
}