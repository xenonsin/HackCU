using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;
using System.Linq;


public class joystick : MonoBehaviour
{
    public static joystick Instance;
    public TextAsset wiiWeight;

    public float TL;
    public float TR;
    public float BL;
    public float BR;

    void Awake()
    {
        Instance = this; 
    }

    void OnDestroy()
    {
        Instance = null;
    }


	
	// Update is called once per frame
	void Update ()
	{
	    string line = "";
        //TextAsset wit = Resources.Load(@"D:\Projects\HackCU\HackCU\Assets\Wii Fit\WiiWeight.txt") as TextAsset;

	    try
	    {
            using (var sw = new StreamReader(@"D:\Projects\HackCU\HackCU\Assets\Wii Fit\WiiWeight.txt"))
            {
                while ((line = sw.ReadLine()) != null)
                {
                    if (line.Contains("TL"))
                    {
                        string[] result = line.Split("="[0]);
                        TL = float.Parse(result[1].Trim());
                    }
                    else if (line.Contains("TR"))
                    {
                        string[] result = line.Split("="[0]);
                        TR = float.Parse(result[1].Trim());
                    }
                    else if (line.Contains("BL"))
                    {
                        string[] result = line.Split("="[0]);
                        BL = float.Parse(result[1].Trim());
                    }
                    else if (line.Contains("BR"))
                    {
                        string[] result = line.Split("="[0]);
                        BR = float.Parse(result[1].Trim());
                    }
                }
            }
	    }
	    catch (IOException e)
	    {
	        
            //Debug.Log(e.Message);
	    }
	}
}
