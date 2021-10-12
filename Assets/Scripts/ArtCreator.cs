using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class ArtCreator : MonoBehaviour
{

    public List<Objects> objList = new List<Objects>();
    private List<Art> artList = new List<Art>();
    public GameObject[] galleryArts;


    private struct Art
    {
        public string ArtTitle;
        public string Subtitle;
        public string Description;
        public int ID;
        public Texture2D ArtTexture;
    }

    // Start is called before the first frame update
    void Start()
    {
        string fileName = Path.Combine(Application.dataPath, "Scripts/data_list.json");
        LoadJson(fileName);
        galleryArts = GameObject.FindGameObjectsWithTag("Art");
       
    }

    // Update is called once per frame
    void Update()
    {
        // update position
        foreach (GameObject a in galleryArts)
        {

            
        }

    }

    public void LoadJson(string fileName)
    {
        using (StreamReader r = new StreamReader(fileName))
        {
            string json = r.ReadToEnd();
            ListItem items = JsonUtility.FromJson<ListItem>(json);
            objList = items.Objects.ToList();
        }
        CreateObj();

    }
    public void CreateObj()
    {
        print("create obj = " + objList.Count);
        // assign object properties with the json data 
        for (int i = 0; i < objList.Count; i++)
        {
            Art p;
            Texture2D art_data = (Texture2D)Resources.Load(objList[i].url);
            p.ArtTexture = art_data;
            p.ID = objList[i].Placement;
            p.ArtTitle = objList[i].Title;
            p.Subtitle = objList[i].Subtitle;
            p.Description = objList[i].Description;

            galleryArts = GameObject.FindGameObjectsWithTag("Art");
            
            // set the art frame with texture
            GameObject art = galleryArts[p.ID-1];
            art.GetComponent<Renderer>().material.mainTexture = art_data;
            art.GetComponent<Proximity>().newTitle = p.ArtTitle;
            art.GetComponent<Proximity>().newSubtitle = p.Subtitle;
            art.GetComponent<Proximity>().newDesc = p.Description;

            artList.Add(p);
        }

    }

}


[Serializable]
public class ListItem
{
    public Objects[] Objects;
}
[Serializable]
public class Objects
{
    public string Title;
    public string Subtitle;
    public string Description;
    public int Placement;
    public string url;
}
