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
    private string description;
    private bool purchased;
    private bool multiple;

    public buyableItem(int w, int r, double m, string n, string d, bool mm)
    {
        wellnessChange = w;
        reputationChange = r;
        moneyChange = m;
        name = n;
        description = d;
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
    public string getDescription() { return description; }
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
            new buyableItem(10, 5, -50, "Microphone", "Increase the sound quality of your videos", false),
            new buyableItem(25, 10, -100, "PC Upgrade", "Increase the preformance of your pc to record better gameplay", false),
            new buyableItem(15, 7, -75, "Monitor", "Upgrade your monitor to ive you more space to work on", false),
            new buyableItem(20, 5, -80, "Gamer Chair", "Buy a new chair to make working mor comfortable", false),
            new buyableItem(10, 5, -25, "Headphones", "Upgrade the sound quality of the games you play", false),
            new buyableItem(10, 5, -30, "New Outfit", "Update your wardrobe to impress your followers", true),
            new buyableItem(10, 5, -40, "New Game", "Buy a new game to play on stream", true)
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
    }

    public buyableItem getItem(int index)
    {
        return buyableItems[index];
    }
}