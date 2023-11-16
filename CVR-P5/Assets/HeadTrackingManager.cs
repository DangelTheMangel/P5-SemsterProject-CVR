using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeadTrackingManager : MonoBehaviour
{
    [SerializeField]
    Transform[] transforms_POI;

    private void Start()
    {
        float[] headRotioan = trackHead();
        writeData("Rotation: " + headRotioan + " Postion: " + transform.position + " Point of interest: " + distanceToPointOfInterest(true));
    }
    private void FixedUpdate()
    {
        float[] headRotioan = trackHead();
        writeData("Rotation: " + headRotioan + " Postion: " + transform.position + " Point of interest: " + distanceToPointOfInterest());

    }
    public string distanceToPointOfInterest(bool first = false) {
        string output = "";
        foreach (Transform t in transforms_POI)
        {
            if (first) {
                output += t.gameObject.name + ";";
            }
            output += Vector3.Angle(transform.forward, t.position) + ";";
        }
        return output;
    }
    public float[] trackHead() {
        float[] rotation = { transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z };
        return rotation;
    }

    public void writeData(string input) {
        Debug.Log(input);
        TextWriter tw = new StreamWriter("C:\\Users\\Christian\\OneDrive\\Skrivebord\\TestCSV.txt",true);

        // write a line of text to the file
        tw.WriteLine(input);

        // close the stream
        tw.Close();
    }
}
