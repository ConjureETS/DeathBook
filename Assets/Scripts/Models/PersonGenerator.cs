using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// Pictures generated from http://uifaces.com
public class PersonGenerator
{
    private const int PICTURES_COUNT = 185;

    private static List<int> pictureIndexesMale;
    private static List<int> pictureIndexesFemale;

    private static NameParser nameParser;

    public struct GeneratedPerson
    {
        public Sprite Picture;
        public string FirstName;
        public string LastName;
    }

    static PersonGenerator()
    {
        nameParser = new NameParser(PICTURES_COUNT);

        // Might be a little heavy, but since it's only done once, it's not that bad
        pictureIndexesMale = new List<int>(PICTURES_COUNT);
        pictureIndexesFemale = new List<int>(PICTURES_COUNT);

        for (int i = 1; i <= PICTURES_COUNT; i++)
        {
            pictureIndexesFemale.Add(i);
            pictureIndexesMale.Add(i);
        }
    }

    public static GeneratedPerson GetGeneratedFemale()
    {
        // Might be a little heavy, but since it's only done once, it's not that bad

        int index = UnityEngine.Random.Range(0, pictureIndexesFemale.Count);

        int picID = pictureIndexesFemale[index];

        pictureIndexesFemale.RemoveAt(index);

        GeneratedPerson person = new GeneratedPerson()
        {
            Picture = Resources.Load<Sprite>(String.Format("ProfilePictures/F_{0}", picID)),
            FirstName = nameParser.GetFemaleFirstName(index),
            LastName = nameParser.GetFemaleLastName(index)
        };

        return person;
    }

    public static GeneratedPerson GetGeneratedMale()
    {
        // Might be a little heavy, but since it's only done once, it's not that bad

        int index = UnityEngine.Random.Range(0, pictureIndexesMale.Count);

        int picID = pictureIndexesMale[index];

        pictureIndexesMale.RemoveAt(index);

        GeneratedPerson person = new GeneratedPerson()
        {
            Picture = Resources.Load<Sprite>(String.Format("ProfilePictures/M_{0}", picID)),
            FirstName = nameParser.GetMaleFirstName(index),
            LastName = nameParser.GetMaleLastName(index)
        };

        return person;
    }
}
