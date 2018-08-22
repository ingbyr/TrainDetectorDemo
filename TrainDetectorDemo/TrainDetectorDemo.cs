using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TrainDetectorDll;

namespace TrainDetectorDemo
{
    class TrainDetectorDemo
    {

        static void TestTrainDector()
        {
            var msgQueue = new FixedSizedQueue<TrainDetector.StepResult>(1000);
            TrainDetector trainDector = new TrainDetector()
            {
                Trainer = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet.exe",
                NetCfg = "E:\\Pictures\\train\\cfg\\yolov3.cfg",
                NetWeights = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet53.conv.74",
                TrainDataPath = "E:\\Pictures\\train\\file",
                DataFilePath = "E:\\Pictures\\train\\train.data",
                Iteration = 5,
                MsgQueue = msgQueue
            };

            TrainDetector.DataFile dataFile;
            dataFile.Classes = 3;
            dataFile.Train = "E:\\Pictures\\train\\train.txt";
            dataFile.Valid = "E:\\Pictures\\train\\train.txt";
            dataFile.Names = "E:\\Pictures\\train\\train.names";
            dataFile.Backup = "E:\\Pictures\\train\\backup";

            trainDector.prepareData(dataFile);

            trainDector.startTrain();

            Console.WriteLine();
            Console.WriteLine("start training...");
            TrainDetector.StepResult result;
            while (trainDector.IsTraining)
            {
                while (msgQueue.TryDequeue(out result)) Console.WriteLine(result.ToString());
            }

            //File.WriteAllLines("E:\\Pictures\\train\\result.txt", results);
        }

        static void ParseOoutputTest()
        {
            var lines = File.ReadAllLines("E:\\Pictures\\train\\result.txt");
            foreach (var line in lines)
            {
                // 1: 847.762512, 847.762512 avg loss, 0.000000 rate, 7.069180 seconds, 64 images
                if(line.EndsWith("images"))
                {
                    var results = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("iterations: " + results[0].Split(':')[0]);
                    Console.WriteLine("avg loss: " + results[1].Split(' ')[0]);
                    Console.WriteLine("rate: " + results[2].Split(' ')[0]);
                    Console.WriteLine("seconds: " + results[3].Split(' ')[0]);
                    Console.WriteLine("images: " + results[4].Split(' ')[0]);
                    Console.WriteLine();
                }
            }
        }


        static void Main(string[] args)
        {
            TestTrainDector();
            //ParseOoutputTest();
        }
    }
}
