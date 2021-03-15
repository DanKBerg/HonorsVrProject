using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacardManager : MonoBehaviour
{
    public List<GameObject> placards;
    private int placardNumber = 0;
    public int lastPlacardIndex;

    public void NextPlacard()
    {
        if(placardNumber < lastPlacardIndex)
        {
            placards[placardNumber].SetActive(false);

            placards[placardNumber + 1].SetActive(true);

            placardNumber += 1;
        }
    }

    public void PreviousPlacard()
    {
        if(placardNumber > 0)
        {
            placards[placardNumber].SetActive(false);

            placards[placardNumber - 1].SetActive(true);

            placardNumber -= 1;
        }
        
    }
}
