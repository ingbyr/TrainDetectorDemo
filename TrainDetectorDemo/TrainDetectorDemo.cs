using System;
using System.Collections.Generic;
using TrainDetectorDll;

namespace TrainDetectorDemo
{
    class TrainDetectorDemo
    {

        static void TestTrainDector()
        {

            TrainDetector trainDector = new TrainDetector()
            {
                Trainer = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet.exe",
                //Trainer = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet_no_gpu.exe",
                NetCfg = "E:\\Pictures\\train\\cfg\\yolov3.cfg",
                NetWeights = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet53.conv.74",
                TrainDataPaths = new List<String> { "E:\\Pictures\\train\\file", "E:\\Pictures\\logo" },
                DataFilePath = "E:\\Pictures\\train\\train.data",
                Iteration = 5
            };

            TrainDetector.DataFile dataFile;
            dataFile.Classes = 3;
            dataFile.Train = "E:\\Pictures\\train\\train.txt";
            dataFile.Valid = "E:\\Pictures\\train\\train.txt";
            dataFile.Names = "E:\\Pictures\\train\\train.names";
            dataFile.Backup = "E:\\Pictures\\train\\backup";

            trainDector.debug = true;
            trainDector.prepareData(dataFile);
            trainDector.startTrain();


            TrainDetector.StepResult result;
            while (trainDector.IsTraining || (trainDector.MsgQueue.Count > 0))
            {
                while (trainDector.MsgQueue.TryDequeue(out result))
                {
                    Console.WriteLine(result.ToString());
                    Console.WriteLine("-----------------------------");
                }
            }
        }


        static void Main(string[] args)
        {
            TestTrainDector();
        }
    }
}
