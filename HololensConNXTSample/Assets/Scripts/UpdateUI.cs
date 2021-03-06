﻿/* This script is developed by Samer Boules (samer.boules@ict.nl)
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
    int NOfconnxtDevices;

    int currentRuuviDisplayed = 1;

    #region My Functions
    //Switch to next Ruuvi Data
    void UpdateMenu()
    {
        SetTextsToNextRuuvi();
    }

    //Private function that takes the number of Ruuvi you want to display and update the UI texts accordingly
    private void SetTextsToRuuviID(int RuuviID)
    {
        UpdateFromConNXT _UpdateFromConNXT = gameObject.GetComponent<UpdateFromConNXT>();
        if (_UpdateFromConNXT.Ruuvis != null)
        {
            if (_UpdateFromConNXT.Ruuvis[RuuviID]._temperature!=null)
            {
                //Update the text fields on the gui
                RUUVINameText.text = "Device Index: " + RuuviID.ToString() + "\nDeviceID: " + _UpdateFromConNXT.Ruuvis[RuuviID]._deviceID;
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
            else
            {
                TemperatureTitleText.text = "";
                TemperatureText.text = "";
                HumidityTitleText.text = "";
                HumidityText.text = "";
                PressureTitleText.text = "";
                PressureText.text = "";
                AccelXTitleText.text = "";
                AccelXText.text = "";
                AccelYTitleText.text = "";
                AccelYText.text = "";
                AccelZTitleText.text = "";
                AccelZText.text = "";
                LastUpdatedText.text = "Not a CoLab device!";
            }
        }
    }

    //Public function called from other modules to display the next Ruuvi data on UI
    //Designed to be called on certain controller button or action (example: tab the touchpad)
    public void SetTextsToNextRuuvi()
    {
        currentRuuviDisplayed = currentRuuviDisplayed + 1;        
        if (currentRuuviDisplayed > 16)
        {
            currentRuuviDisplayed = 0;
        }
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
        SetTextsToRuuviID(0);
        InvokeRepeating("UpdateMenu", 0f, 2f);
    }

    void Update()
    {
    }
#endregion
}
