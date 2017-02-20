using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    public Text possesionEnergyLabel;

    public Text solarPowerLabel;
    public Text solarPowerPriceLabel;
    public Text solarRangeLabel;
    public Text solarRangePriceLabel;
    public Text rotationSpeedLabel;
    public Text rotationSpeedPriceLabel;
    public Text brakeSpeedLabel;
    public Text brakeSpeedPriceLabel;
    public Text peopleRegenLabel;
    public Text peopleRegenPriceLabel;
    public Text itemRegenLabel;
    public Text itemRegenPriceLabel;

    float currentEnergy;
    float SolarPowerPriceEnergy;

    void Start()
    {
        ResetText();
    }

    void ResetText()
    {
        solarPowerLabel.text = "Solar Power\nLv "; //+ level;
        solarPowerPriceLabel.text = "Energy"; //insert price
        solarRangeLabel.text = "Solar Range\nLv "; //+ level;
        solarRangePriceLabel.text = "Energy"; //insert price
        rotationSpeedLabel.text = "Rotation Speed\nLv "; //+ level;
        rotationSpeedPriceLabel.text = "Energy"; //insert price
        brakeSpeedLabel.text = "Brake Speed\nLv "; //+ level;
        brakeSpeedPriceLabel.text = "Energy"; //insert price
        peopleRegenLabel.text = "Regen People\nLv "; //+ level;
        peopleRegenPriceLabel.text = "Energy"; //insert price
        itemRegenLabel.text = "Regen Item\nLv "; //+ level;
        itemRegenPriceLabel.text = "Energy"; //insert price
    }

    

    public void SolarPowerButton()
    {
        if (currentEnergy >= SolarPowerPriceEnergy)
        {
//            SoundManager.Instance.ClickSound();
            //upgrade

        }
        else
        {
//            SoundManager.Instance.ErrorSound();
            //need more money
        }
    }

    public void SolarRangeButton()
    {
        if (currentEnergy >= SolarPowerPriceEnergy)
        {
 //           SoundManager.Instance.ClickSound();
            //upgrade

        }
        else
        {
 //           SoundManager.Instance.ErrorSound();
            //need more money
        }
    }

    public void RotationButton()
    {
        if (currentEnergy >= SolarPowerPriceEnergy)
        {
 //           SoundManager.Instance.ClickSound();
            //upgrade

        }
        else
        {
//            SoundManager.Instance.ErrorSound();
            //need more money
        }
    }

    public void BrakeButton()
    {
        if (currentEnergy >= SolarPowerPriceEnergy)
        {
 //           SoundManager.Instance.ClickSound();
            //upgrade

        }
        else
        {
//            SoundManager.Instance.ErrorSound();
            //need more money
        }
    }

    public void RegenPeopleButton()
    {
        if (currentEnergy >= SolarPowerPriceEnergy)
        {
//            SoundManager.Instance.ClickSound();
            //upgrade

        }
        else
        {
 //           SoundManager.Instance.ErrorSound();
            //need more money
        }
    }

    public void RegenItemButton()
    {
        if (currentEnergy >= SolarPowerPriceEnergy)
        {
 //           SoundManager.Instance.ClickSound();
            //upgrade

        }
        else
        {
 //           SoundManager.Instance.ErrorSound();
            //need more money
        }
    }
    

    public void GoToNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
