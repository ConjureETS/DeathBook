using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//www.uifaces.com
public class PictureGenerator
{
    private const int PICTURES_COUNT = 185;

    private static List<int> pictureIndexesMale;
    private static List<int> pictureIndexesFemale;

    static PictureGenerator()
    {
        // Might be a little heavy, but since it's only done once, it's not that bad
        pictureIndexesMale = new List<int>(PICTURES_COUNT);
        pictureIndexesFemale = new List<int>(PICTURES_COUNT);

        for (int i = 0; i < PICTURES_COUNT; i++)
        {
            pictureIndexesFemale.Add(i);
            pictureIndexesMale.Add(i);
        }
    }

    public static Texture GetFemalePicture()
    {
        // Might be a little heavy, but since it's only done once, it's not that bad

        int index = UnityEngine.Random.Range(1, pictureIndexesFemale.Count);

        int picID = pictureIndexesFemale[index];

        pictureIndexesFemale.RemoveAt(index);

        return Resources.Load(String.Format("ProfilePictures/F_{0}", picID)) as Texture;
    }

    public static Texture GetMalePicture()
    {
        // Might be a little heavy, but since it's only done once, it's not that bad

        int index = UnityEngine.Random.Range(1, pictureIndexesMale.Count);

        int picID = pictureIndexesMale[index];

        pictureIndexesMale.RemoveAt(index);

        return Resources.Load(String.Format("ProfilePictures/M_{0}", picID)) as Texture;
    }
}
