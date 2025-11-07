using UnityEngine;
using System.IO;
using System.Globalization;

using Microsoft.Extensions.Configuration;

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

    public static int speed_mult = 1;

    public static float circle_size = 30f;
    public static float circle_x = -30f;
    public static float circle_y = 60f;
    public static float circle_black_time = 0.5f;

    public static int cameraMode = 0;
    public static float xOffset = 25;
    public static Vector3 bugInitPosition = new Vector3(0f, 0.1f, 50f - 46.4f);
    public static Vector3 cameraOffset = new Vector3(0f, 2.5f, 0f);

    private static float LoadFloat(IConfiguration configuration, string name)
    {
        return float.Parse(configuration[name], CultureInfo.InvariantCulture);
    }

    public static void Load()
    {
        if (isLoaded)
        {
            return;
        }

        isLoaded = true;

        try
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("configuration.ini", optional: false, reloadOnChange: true)
                .Build();

            Debug.Log("conf loaded from ini");

            cherry_size = LoadFloat(configuration, "Objects:cherry_size");
            wasp_size = LoadFloat(configuration, "Objects:wasp_size");
            cherry_speed = LoadFloat(configuration, "Objects:cherry_speed");
            wasp_speed = LoadFloat(configuration, "Objects:wasp_speed");
            bug_speed = LoadFloat(configuration, "Objects:bug_speed");
            bug_rotate_speed = LoadFloat(configuration, "Objects:bug_rotate_speed");

            start_pause = LoadFloat(configuration, "Time:start_pause");

            circle_size = LoadFloat(configuration, "EEG:circle_size");
            circle_x = LoadFloat(configuration, "EEG:circle_x");
            circle_y = LoadFloat(configuration, "EEG:circle_y");
            circle_black_time = LoadFloat(configuration, "EEG:circle_black_time");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

    }
}
