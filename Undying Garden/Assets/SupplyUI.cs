using UnityEngine;
using UnityEngine.UI;

public class SupplyUI : MonoBehaviour
{
    public Text arrowSupplyText;
    public Text trapSupplyText;

    // Update is called once per frame
    void Update()
    {
        arrowSupplyText.text = ("Arrows: ") + SupplyCount.arrowCount.ToString();
        trapSupplyText.text = ("Traps: ") + SupplyCount.trapCount.ToString();
    }
}
