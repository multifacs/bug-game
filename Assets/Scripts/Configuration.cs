using UnityEngine;
using System.IO;
using System.Globalization;

public class Configuration
{
    private static bool isLoaded = false;
    public static float cherry_size = 1f;
    public static float wasp_size = 1f;
    public static float cherry_speed = 1f;
    public static float wasp_speed = 1f;
    public static float bug_speed = 1f;
    public static float bug_rotate_speed = 1f;
    public static float start_pause = 5f;
    public static int cameraMode = 0;
    public static float xOffset = 25;
    public static Vector3 bugInitPosition = new Vector3(0f, 0.1f, 50f - 46.4f);
    public static Vector3 cameraOffset = new Vector3(0f, 2.5f, 0f);

    public void Load()
    {
        if (isLoaded)
        {
            return;
        }

        isLoaded = true;
        Debug.Log("Load configuration.txt start");
        var path = Directory.GetCurrentDirectory();
        Debug.Log(path);

        StreamReader reader = new StreamReader(path + "/configuration.txt", true);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var tokens = line.Split('=');

            Debug.Log(line);
            foreach (var t in tokens)
            {
                Debug.Log("[" + t + "]");
            }

//            Array.ForEach(tokens, delegate(string s) { s.Trim(); });

            if (tokens.Length == 2)
            {
                Debug.Log("tokens.Length == 2");
                Debug.Log("tokens[0] = [" + tokens[0] + "]");

                if (tokens[0].Trim().Equals("cherry_size"))
                {
                    cherry_size = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Cherry_size is load as : " + cherry_size);
                }
                else if (tokens[0].Trim().Equals("wasp_size"))
                {
                    wasp_size = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Wasp_size is load as : " + wasp_size);
                }
                else if (tokens[0].Trim().Equals("cherry_speed"))
                {
                    cherry_speed = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Cherry speed is load as : " + cherry_speed);
                }
                else if (tokens[0].Trim().Equals("wasp_speed"))
                {
                    wasp_speed = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Wasp_speed is load as : " + wasp_speed);
                }
                else if (tokens[0].Trim().Equals("bug_speed"))
                {
                    bug_speed = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Bug_speed is load as : " + bug_speed);
                }
                else if (tokens[0].Trim().Equals("bug_rotate_speed"))
                {
                    bug_rotate_speed = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Bug_rotate_speed is load as : " + bug_rotate_speed);
                }
                else if (tokens[0].Trim().Equals("start_pause"))
                {
                    start_pause = float.Parse(tokens[1].Trim().Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
                    Debug.Log("Start_pause is load as : " + start_pause);
                }
            }
            //           var scene = int.Parse(tokens[0]);

        }
    }
}
