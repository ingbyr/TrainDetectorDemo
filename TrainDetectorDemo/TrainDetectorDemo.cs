using System;
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
                Iteration = 20,
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


        static void Main(string[] args)
        {
            TestTrainDector();
        }
    }
}
