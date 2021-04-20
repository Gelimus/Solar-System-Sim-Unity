using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    Slider timeScaleSlider;
    float timeScaleRange = GlobalSettings.timeScaleMax - GlobalSettings.timeScaleMin;

    Text currentTimeScaleLabel;

    Slider planetSizeScaleSlider;
    float planetSizeScaleRange = GlobalSettings.bodiesScaleMax - GlobalSettings.bodiesScaleMin;

    Text currentPlanetSizeScaleLabel;


    Slider starSizeScaleSlider;
    float starSizeScaleRange = GlobalSettings.starScaleMax - GlobalSettings.starScaleMin;

    Text currentStarSizeScaleLabel;

    Dropdown focusTarget;

    InputField massMultiplierInputField;
    Text massMultiplierLabel;

    AstronomicalObject focus;

    Text CurrentDateLabel;

    DateTime currentDate;

    // Start is called before the first frame update
    void Start()
    {
        SetUpTimeScaleControl();

        SetUpPlanetaryScaleControl();

        SetUpStarScaleControl();

        SetUpFocusControl();

        SetUpMassMultiplication();

        SetUpTimeDisplay();
    }

    private void SetUpTimeDisplay()
    {
        CurrentDateLabel = GameObject.Find("CurrentDate").GetComponent<Text>();
        currentDate = StellarConstants.ConvertToGregorian(GlobalSettings.startTimeInJulianDays);
        CurrentDateLabel.text = currentDate.ToString();
    }

    private void SetUpMassMultiplication()
    {
        massMultiplierInputField = GameObject.Find("MassMultiplierInputField").GetComponent<InputField>();
        massMultiplierLabel = GameObject.Find("MassMultiplierLabel").GetComponent<Text>();
    }

    private void SetUpFocusControl()
    {
        focusTarget = GameObject.Find("SelectFocus").GetComponent<Dropdown>();
        focusTarget.options.Add(new Dropdown.OptionData("No focus"));
        foreach (AstronomicalObject ao in PlanetarySystem.main.SystemElements)
        {
            focusTarget.options.Add(new Dropdown.OptionData(ao.name));
        }
        focusTarget.onValueChanged.AddListener(delegate { FocusChanged(focusTarget.options[focusTarget.value].text); });
    }

    private void SetUpStarScaleControl()
    {
        starSizeScaleSlider = GameObject.Find("Star Scale Slider").GetComponent<Slider>();
        currentStarSizeScaleLabel = GameObject.Find("CurrentStarScale").GetComponent<Text>();
        starSizeScaleSlider.onValueChanged.AddListener(delegate { StarSizeScaleSliderValueChanged(); });
        starSizeScaleSlider.onValueChanged.AddListener(delegate { PlanetarySystem.main.resizeBodies(); });
        UpdateStarSizeTimeScaleLabel();
    }

    private void SetUpPlanetaryScaleControl()
    {
        planetSizeScaleSlider = GameObject.Find("Planet Scale Slider").GetComponent<Slider>();
        currentPlanetSizeScaleLabel = GameObject.Find("CurrentPlanetScale").GetComponent<Text>();
        planetSizeScaleSlider.onValueChanged.AddListener(delegate { PlanetSizeScaleSliderValueChanged(); });
        planetSizeScaleSlider.onValueChanged.AddListener(delegate { PlanetarySystem.main.resizeBodies(); });
        UpdatePlanetSizeTimeScaleLabel();
    }

    private void SetUpTimeScaleControl()
    {
        timeScaleSlider = GameObject.Find("Time Scale Slider").GetComponent<Slider>();
        currentTimeScaleLabel = GameObject.Find("CurrentTimeScale").GetComponent<Text>();

        timeScaleSlider.onValueChanged.AddListener(delegate { TimeScaleSliderValueChanged(); });
        UpdateTimeScaleLabel();
    }



    // Update is called once per frame
    void Update()
    {
        UpdateCurrentDateLabel();
    }

    private void UpdateCurrentDateLabel()
    {
        
        currentDate = currentDate.AddSeconds(Time.deltaTime*GlobalSettings.timeScale);
        CurrentDateLabel.text = currentDate.ToString();
    }

    private void TimeScaleSliderValueChanged()
    {
        GlobalSettings.timeScale = GlobalSettings.timeScaleMin + timeScaleSlider.value * timeScaleRange;
        UpdateTimeScaleLabel();
    }

    private void UpdateTimeScaleLabel()
    {
        int daysScale = Mathf.RoundToInt(GlobalSettings.timeScale / GlobalSettings.timeScaleMin);
        currentTimeScaleLabel.text = "1 s = "+ daysScale + " days";
    }

    private void PlanetSizeScaleSliderValueChanged()
    {
        GlobalSettings.bodiesSizeScale = GlobalSettings.bodiesScaleMin + planetSizeScaleSlider.value * planetSizeScaleRange;
        UpdatePlanetSizeTimeScaleLabel();
    }

    private void UpdatePlanetSizeTimeScaleLabel()
    {
        int sizeScale = Mathf.RoundToInt(GlobalSettings.bodiesSizeScale);
        currentPlanetSizeScaleLabel.text = "Planets size x" + sizeScale;
    }

    private void StarSizeScaleSliderValueChanged()
    {
        GlobalSettings.starScale = GlobalSettings.starScaleMin + starSizeScaleSlider.value * starSizeScaleRange;
        UpdateStarSizeTimeScaleLabel();
    }

    private void UpdateStarSizeTimeScaleLabel()
    {
        int sizeScale = Mathf.RoundToInt(GlobalSettings.starScale);
        currentStarSizeScaleLabel.text = "Star size x" + sizeScale;
    }

    private void FocusChanged(string name)
    {
        foreach (AstronomicalObject ao in PlanetarySystem.main.SystemElements)
        {
            if (!ao.name.Equals(name))
            {
                ao.LightUp(false);
                continue;
            }
            ao.LightUp(true);
            focus = ao;
            CameraController.cameraController.SetCameraTarget(ao.transform.position);
        }
    }


    public void OnMassMultiplierConfirmButtonClicked()
    {
        Debug.Log("I am here");
        Debug.Log(massMultiplierInputField.text);
        try
        {
            if (focusTarget == null)
            {
                massMultiplierInputField.text = "No Focus Selected";
                return;
            }
            focus.SetMassMultiplier(Convert.ToDouble(massMultiplierInputField.text));
            massMultiplierLabel.text = "x "+ massMultiplierInputField.text;
        }
        catch (FormatException)
        {
            massMultiplierInputField.text = "Incorrect input";
        }
        
    }

    public void OnResetButtonClicked()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(index,LoadSceneMode.Single);

    }
}
