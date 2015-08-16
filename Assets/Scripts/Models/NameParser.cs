using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

// Names generated from http://www.uinames.com
public class NameParser
{
    private string[] maleFirstNames;
    private string[] femaleFirstNames;

    private string[] maleLastNames;
    private string[] femaleLastNames;

    public NameParser(int count)
    {
        maleFirstNames = new string[count];
        femaleFirstNames = new string[count];

        maleLastNames = new string[count];
        femaleLastNames = new string[count];

        TextAsset femaleJson = Resources.Load("TextFiles/WomenNames") as TextAsset;
        TextAsset maleJson = Resources.Load("TextFiles/MenNames") as TextAsset;

        var parsedFemales = JSON.Parse(femaleJson.text);
        var parsedMales = JSON.Parse(maleJson.text);

        for (int i = 0; i < count; i++)
        {
            femaleFirstNames[i] = parsedFemales[i]["name"];
            femaleLastNames[i] = parsedFemales[i]["surname"];

            maleFirstNames[i] = parsedMales[i]["name"];
            maleLastNames[i] = parsedMales[i]["surname"];
        }
    }

    public string GetFemaleFirstName(int index)
    {
        return femaleFirstNames[index];
    }

    public string GetMaleFirstName(int index)
    {
        return maleFirstNames[index];
    }

    public string GetFemaleLastName(int index)
    {
        return femaleLastNames[index];
    }

    public string GetMaleLastName(int index)
    {
        return maleLastNames[index];
    }
}
