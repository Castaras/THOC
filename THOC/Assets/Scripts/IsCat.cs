using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCat : MonoBehaviour {

    private static List<IsCat> catList = new List<IsCat>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public static List<IsCat> GetCatList
    {
        get { return catList; }
    }
    void Awake()
    {
        catList.Add(this);
    }

    void OnDestroy()
    {
        if (catList.Contains(this))
        {
            catList.Remove(this);
        }
    }


}
