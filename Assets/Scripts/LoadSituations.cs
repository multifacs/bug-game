using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;



public class LoadSituations
{
    public struct Data {
        public Data(int scene, bool waspVisible, 
            int waspX, int waspY, float waspDx, float waspDy, float waspVx, float waspVy, 
            int cherryX, int cherryY, float cherryDx, float cherryDy, float cherryVx, float cherryVy)
        {
            this.scene = scene;
            this.waspVisible = waspVisible;
            this.waspX = waspX;
            this.waspY = waspY;
            this.waspDx = waspDx;
            this.waspDy = waspDy;
            this.waspVx = waspVx;
            this.waspVy = waspVy;
            this.cherryX = cherryX;
            this.cherryY = cherryY;
            this.cherryDx = cherryDx;
            this.cherryDy = cherryDy;
            this.cherryVx = cherryVx;
            this.cherryVy = cherryVy;
        }

        public int scene;
        public bool waspVisible;
        public int waspX;
        public int waspY;
        public float waspDx;
        public float waspDy;
        public float waspVx;
        public float waspVy;
        public int cherryX;
        public int cherryY;
        public float cherryDx;
        public float cherryDy;
        public float cherryVx;
        public float cherryVy;
    }

    public static List<Data> datas = new List<Data>();
    private static bool isLoaded = false;
    private static bool initLog = InitLog();
    private static StreamWriter writer;

    public static bool showTrees = true;
    public static bool showHills = true;
    private static long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();


    private static bool InitLog()
    {
        var path = Directory.GetCurrentDirectory();
        DateTime dt = DateTime.Now;

        writer = new StreamWriter(path + "/path_game_log_" + dt.ToString("yyyyMMdd-HHmmss") + ".txt");
        writer.WriteLine("time\tbug_X\tbug_Y\twasp->wx\twasp->wy\twasp->d_x\twasp->d_y\twasp->d_vx\twasp->d_vy\tcherry->cx\tcherry->cy\tcherry->d_cx\tcherry->d_cy\tlevel\tscore\tscene_number\tattempt\teyetr_X\teyetr_Y");
        writer.Flush();

        return true;
    }


    public static void CloseLog()
    {
        writer.Flush();
        writer.Close();
        Debug.Log("Log stream closed");
    }

    private static CultureInfo ci = new CultureInfo("en-US");
    private static string F4L(float num)
    {
        
        return num.ToString("0.###", ci);
    }

    public static void WriteLog(float bugX, float bugY, 
        float waspX, float waspY, 
        float waspDX, float waspDY, 
        float waspVX, float waspVY,
        float cherryX, float cherryY,
        float cherryDX, float cherryDY,
        float level, float score, float scene, float attempt,
        float eyeX, float eyeY
        )
    {
        writer.WriteLineAsync((DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime) + "\t" 
            + F4L(bugX) + "\t" + F4L(500 - bugY) + "\t" 
            + F4L(waspX) + "\t" + F4L(500 - waspY) + "\t"
            + F4L(waspDX) + "\t" + F4L(waspDY) + "\t"
            + F4L(waspVX) + "\t" + F4L(waspVY) + "\t"
            + F4L(cherryX) + "\t" + F4L(500 - cherryY) + "\t"
            + F4L(cherryDX) + "\t" + F4L(cherryDY) + "\t"

            + F4L(level) + "\t" + F4L(score) + "\t"
            + F4L(scene) + "\t" + F4L(attempt) + "\t"
            + F4L(eyeX) + "\t" + F4L(eyeY)
            );
    }

    public void Load()
    {
        if (isLoaded)
        {
            return;
        }

        isLoaded = true;
        Debug.Log("LoadSituations start");
        var path = Directory.GetCurrentDirectory();
        Debug.Log(path);

        StreamReader reader = new StreamReader(path + "/situations.txt", true);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var tokens = line.Split('\t');

            Debug.Log(line);
            foreach (var t in tokens)
            {
                Debug.Log(t);
            }

            var scene = int.Parse(tokens[0]);
            var waspVisible = int.Parse(tokens[1]) == 0 ? false : true;
            var waspX = int.Parse(tokens[2]);
            var waspY = int.Parse(tokens[3]);
            var waspDx = float.Parse(tokens[4].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var waspDy = float.Parse(tokens[5].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var waspVx = float.Parse(tokens[6].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var waspVy = float.Parse(tokens[7].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var cherryX = int.Parse(tokens[8]);
            var cherryY = int.Parse(tokens[9]);
            var cherryDx = float.Parse(tokens[10].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var cherryDy = float.Parse(tokens[11].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var cherryVx = float.Parse(tokens[12].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));
            var cherryVy = float.Parse(tokens[13].Replace('.', NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]));

            datas.Add(new Data(scene, waspVisible, 
                waspX, waspY, waspDx, waspDy, waspVx, waspVy, 
                cherryX, cherryY, cherryDx, cherryDy, cherryVx, cherryVy));

        }
    }



}
