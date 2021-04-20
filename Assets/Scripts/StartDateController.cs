using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartDateController : MonoBehaviour
{
    Dropdown day;
    Dropdown month;
    Dropdown year;
    // Start is called before the first frame update
    void Start()
    {
        day = GameObject.Find("Day").GetComponent<Dropdown>();
        month = GameObject.Find("Month").GetComponent<Dropdown>();
        month.onValueChanged.AddListener(delegate { UpdateDays(); });
        year = GameObject.Find("Year").GetComponent<Dropdown>();
        year.onValueChanged.AddListener(delegate { UpdateDays(); });

        for (int i = 1; i <= 31; i++)
        {
            day.options.Add(new Dropdown.OptionData(i.ToString()));
        }

        for (int i = 2000; i < 2030; i++)
        {
            year.options.Add(new Dropdown.OptionData(i.ToString()));
        }

        for (int i = 1; 1 <= 12; i++)
        {
            month.options.Add(new Dropdown.OptionData(new DateTime(2000, i, 1).ToString("MMM", CultureInfo.InvariantCulture)));
        }

        
    }

    private void UpdateDays()
    {
        day.options = new List<Dropdown.OptionData>();
        for (int i = 1; i <= DateTime.DaysInMonth(Int32.Parse(year.options[year.value].text),month.value+1); i++)
        {
            day.options.Add(new Dropdown.OptionData(i.ToString()));
        }
    }


    public void OnBeginButtonClicked()
    {
        int D = day.value + 1;
        int M = month.value + 1;
        int Y = year.value + 2000;

        int JDN = (1461 * (Y + 4800 + (M - 14) / 12)) / 4 + (367 * (M - 2 - 12 * ((M - 14) / 12))) / 12 - (3 * ((Y + 4900 + (M - 14) / 12) / 100)) / 4 + D - 32075;

        GlobalSettings.startTimeInJulianDays = JDN;

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
