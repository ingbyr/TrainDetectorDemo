using System;
using TrainDetectorDll;

namespace TrainDetectorDemo
{
    class TrainDetectorDemo
    {
        static void Main(string[] args)
        {
            
            TrainDetector trainDector = new TrainDetector();
            trainDector.trainer = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet.exe";
            trainDector.netCfg = "E:\\Pictures\\train\\cfg\\yolov3.cfg";
            trainDector.netWeights = "E:\\VSProjects\\darknet\\build\\darknet\\x64\\darknet53.conv.74";
            trainDector.trainDataPath = "E:\\Pictures\\train\\file";
            trainDector.dataFilePath = "E:\\Pictures\\train\\train.data";

            TrainDetector.DataFile dataFile;
            dataFile.classes = 3;
            dataFile.train = "E:\\Pictures\\train\\train.txt";
            dataFile.valid = "E:\\Pictures\\train\\train.txt";
            dataFile.names = "E:\\Pictures\\train\\train.names";
            dataFile.backup = "E:\\Pictures\\train\\backup";

            trainDector.prepareData(dataFile);
            trainDector.startTrain();
            Console.WriteLine("press any key to quit");
            Console.ReadKey();
            trainDector.stopProcess();
        }
    }
}
