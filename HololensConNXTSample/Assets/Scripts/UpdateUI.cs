/* This script is developed by Samer Boules (samer.boules@ict.nl)
 * It's function is update the menu with RUUVI data. Connection to ConNXT and the actual update of data is done in PersistenceExample.cs
 * Managing the switch to the next menu is done by PersistentBall.cs
 * 18 March 2019
 */

//#define SIMULATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using UnityEditor;

public class UpdateUI : MonoBehaviour
{
    #region Public Variables
    //Declare UI text boxes that will be updated
    public Text RUUVINameText = null;
    public Text TemperatureTitleText = null;
    public Text TemperatureText = null;
    public Text HumidityTitleText = null;
    public Text HumidityText = null;
    public Text PressureTitleText = null;
    public Text PressureText = null;
    public Text AccelXTitleText = null;
    public Text AccelXText = null;
    public Text AccelYTitleText = null;
    public Text AccelYText = null;
    public Text AccelZTitleText = null;
    public Text AccelZText = null;
    public Text LastUpdatedText = null;
    #endregion

    public float currentTemperature;
    int NUMBER_OF_RUUVIS = 12;

    int currentRuuviDisplayed = 1;


    #region My Functions
    //Switch to next Ruuvi Data
    void UpdateMenu()
    {
        SetTextsToNextRuuvi();
    }

    //Private function that takes the number of Ruuvi you want to display and update the UI texts accordingly
    //RuuviID range: from 1 to 12 (Don't send in 0 because this is the Raspberry Pi gateway which has no telemetry
    private void SetTextsToRuuviID(int RuuviID)
    {
        if (RuuviID == 0 || currentRuuviDisplayed==0 || RuuviID > NUMBER_OF_RUUVIS || currentRuuviDisplayed > NUMBER_OF_RUUVIS)
        {
            RuuviID = 1;
            currentRuuviDisplayed = 1;
        }
            UpdateFromConNXT _UpdateFromConNXT = gameObject.GetComponent<UpdateFromConNXT>();
        if (_UpdateFromConNXT.Ruuvis != null)
        {
            //Update the text fields on the gui
            RUUVINameText.text = "CoLab RUUVI Tag 00" + RuuviID.ToString() + "\nDeviceID: " + _UpdateFromConNXT.Ruuvis[RuuviID]._deviceID;
            TemperatureTitleText.text = "Temperature";
            currentTemperature = float.Parse(_UpdateFromConNXT.Ruuvis[RuuviID]._temperature, System.Globalization.CultureInfo.InvariantCulture);

            TemperatureText.text = _UpdateFromConNXT.Ruuvis[RuuviID]._temperature + " °C";
            HumidityTitleText.text = "Humidity";
            HumidityText.text = _UpdateFromConNXT.Ruuvis[RuuviID]._humidity + " %";
            PressureTitleText.text = "Pressure";
            PressureText.text = _UpdateFromConNXT.Ruuvis[RuuviID]._pressure + " hPa";
            AccelXTitleText.text = "Acceleration X";
            AccelXText.text = _UpdateFromConNXT.Ruuvis[RuuviID]._accelerationX + " m/s2";
            AccelYTitleText.text = "Acceleration Y";
            AccelYText.text = _UpdateFromConNXT.Ruuvis[RuuviID]._accelerationY + " m/s2";
            AccelZTitleText.text = "Acceleration Z";
            AccelZText.text = _UpdateFromConNXT.Ruuvis[RuuviID]._accelerationZ + " m/s2"; ;
            LastUpdatedText.text = "Last updated on " + _UpdateFromConNXT.Ruuvis[RuuviID]._timeStamp;
        }
    }

    //Public function called from other modules to display the next Ruuvi data on UI
    //Designed to be called on certain controller button or action (example: tab the touchpad)
    public void SetTextsToNextRuuvi()
    {
            currentRuuviDisplayed = currentRuuviDisplayed + 1;
            SetTextsToRuuviID(currentRuuviDisplayed);         
    }


    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
#endregion


#region Unity Functions
    void Start()
    {
        SetTextsToRuuviID(currentRuuviDisplayed);
        InvokeRepeating("UpdateMenu", 0f, 10f);
    }

    void Update()
    {
    }
#endregion
}
