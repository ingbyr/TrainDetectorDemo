using System;
using TrainDetectorDll;
using System.Threading;

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
                TrainDataPath = "E:\\Pictures\\train\\file",
                DataFilePath = "E:\\Pictures\\train\\train.data",
                Iteration = 2
            };

            TrainDetector.DataFile dataFile;
            dataFile.Classes = 3;
            dataFile.Train = "E:\\Pictures\\train\\train.txt";
            dataFile.Valid = "E:\\Pictures\\train\\train.txt";
            dataFile.Names = "E:\\Pictures\\train\\train.names";
            dataFile.Backup = "E:\\Pictures\\train\\backup";

            trainDector.debug = false;
            trainDector.prepareData(dataFile);

            var train = new Thread(() =>
            {
                trainDector.startTrain();
            });
            train.Start();

            TrainDetector.StepResult result;
            while (train.IsAlive)
            {
                while (trainDector.IsTraining || (trainDector.MsgQueue.Count > 0))
                {
                    while (trainDector.MsgQueue.TryDequeue(out result))
                    {
                        Console.WriteLine(result.ToString());
                        Console.WriteLine("-----------------------------");
                    }
                }
            }

            //File.WriteAllLines("E:\\Pictures\\train\\result.txt", results);
        }


        static void Main(string[] args)
        {
            TestTrainDector();
        }
    }
}
