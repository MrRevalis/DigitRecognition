using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DigitRecognition.Core
{
    public class Network
    {
        public double LearningRate { get; set; }
        public double Momentum { get; set; }
        public Layer EntryLayer { get; set; }
        public List<Layer> HiddenLayers { get; set; }
        public Layer ExitLayer { get; set; }
        public string DefaultNetworkPath { get; } = @"..\..\..\DefaultNetwork\defaultNetwork.json";
        public string LayerDescription { get; set; }
        public Network()
        {
            if (File.Exists(DefaultNetworkPath))
            {
                Network defaultNetwork = ImportNetwork.NetworkImport(DefaultNetworkPath);

                LearningRate = defaultNetwork.LearningRate;
                Momentum = defaultNetwork.Momentum;
                EntryLayer = defaultNetwork.EntryLayer;
                HiddenLayers = defaultNetwork.HiddenLayers;
                ExitLayer = defaultNetwork.ExitLayer;

                foreach(var x in HiddenLayers)
                {
                    LayerDescription += x.Neurons.Count + ",";
                }
                LayerDescription = LayerDescription.Remove(LayerDescription.Length - 1);
            }
            else
            {
                LearningRate = 0;
                Momentum = 0;
                EntryLayer = new Layer();
                HiddenLayers = new List<Layer>();
                ExitLayer = new Layer();
            }
        }

        public Network(bool temp)
        {
            LearningRate = 0;
            Momentum = 0;
            EntryLayer = new Layer();
            HiddenLayers = new List<Layer>();
            ExitLayer = new Layer();
        }

        public void ChangeNetworkSettings(string neuronsNumber)
        {
            if (neuronsNumber.Last() == ',')
            {
                neuronsNumber = neuronsNumber.Remove(neuronsNumber.Length - 1);
            }

            LayerDescription = neuronsNumber;

            string[] data = neuronsNumber.Split(new[] { ',' }).ToArray();
            int[] entryData = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                entryData[i] = int.Parse(data[i]);
            }

            LearningRate = 0.05;
            Momentum = 0.1;

            EntryLayer = new Layer();
            HiddenLayers = new List<Layer>();
            ExitLayer = new Layer();

            EntryLayer = EntryLayer.CreateLayer(784);

            var firstLayer = new Layer();

            for (int i = 0; i < entryData[0]; i++)
            {
                firstLayer.Neurons.Add(new Neuron(EntryLayer));
            }
            HiddenLayers.Add(firstLayer);

            for (int i = 1; i < entryData.Length; i++)
            {
                Layer hiddenLayer = new Layer();
                for (int j = 0; j < entryData[i]; j++)
                {
                    hiddenLayer.Neurons.Add(new Neuron(HiddenLayers[i - 1]));
                }
                HiddenLayers.Add(hiddenLayer);
            }

            for (int i = 0; i < 10; i++)
            {
                ExitLayer.Neurons.Add(new Neuron(HiddenLayers.Last()));
            }
        }

        public string Learn(List<DataSet> dataSet, CancellationToken cancellationToken)
        {
            List<double> listOfErrors = new List<double>();

            Shuffle(dataSet);

            foreach(var x in dataSet)
            {
                ForwardPropagation(x.Brightness);
                BackPropagation(x.Name);
                listOfErrors.Add(CalculateError(x.Name));

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
            double error = listOfErrors.Average();

            ExportNetwork.Export(this);
            return $"Blad => {error}";
        }

        private void ForwardPropagation(params double[] wejscie)
        {
            var i = 0;
            for (int j = 0; j < EntryLayer.Neurons.Count; j++)
            {
                EntryLayer.Neurons[i].Value = wejscie[i++];
            }
            HiddenLayers.ForEach(x => x.Neurons.ForEach(y => y.CalculateEntry()));
            ExitLayer.Neurons.ForEach(x => x.CalculateEntry());
        }

        private void BackPropagation(params double[] cel)
        {
            var i = 0;
            ExitLayer.Neurons.ForEach(x => x.CalculateGradient(cel[i++]));
            HiddenLayers.Reverse();
            HiddenLayers.ForEach(x => x.Neurons.ForEach(y => y.CalculateGradient()));
            HiddenLayers.ForEach(x => x.Neurons.ForEach(y => y.UpdateWeight(LearningRate, Momentum)));
            HiddenLayers.Reverse();
            ExitLayer.Neurons.ForEach(x => x.UpdateWeight(LearningRate, Momentum));
        }

        public double[] Calculate(params double[] entry)
        {
            ForwardPropagation(entry);
            return ExitLayer.Neurons.Select(x => x.Value).ToArray();
        }

        public double CalculateError(params double[] goal)
        {
            var i = 0;
            return ExitLayer.Neurons.Sum(x => Math.Abs(x.CalculateError(goal[i++])));
        }

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RandomClass.RandomWeight(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
