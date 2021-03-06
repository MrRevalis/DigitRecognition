﻿using System;
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
        private string DefaultNetworkPath { get; } = @"..\..\..\DefaultNetwork\defaultNetwork.json";
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
                LearningRate = 0.05;
                Momentum = 0.1;
                EntryLayer = new Layer();
                HiddenLayers = new List<Layer>();
                ExitLayer = new Layer();
            }
        }

        public Network(bool temp)
        {
            LearningRate = 0.05;
            Momentum = 0.1;
            EntryLayer = new Layer();
            HiddenLayers = new List<Layer>();
            ExitLayer = new Layer();
        }

        public void LoadNetwork(Network network)
        {
            LearningRate = network.LearningRate;
            Momentum = network.Momentum;
            EntryLayer = network.EntryLayer;
            HiddenLayers = network.HiddenLayers;
            ExitLayer = network.ExitLayer;

            foreach (var x in HiddenLayers)
            {
                LayerDescription += x.Neurons.Count + ",";
            }
            LayerDescription = LayerDescription.Remove(LayerDescription.Length - 1);
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

            dataSet.ShuffleData();

            foreach(var x in dataSet)
            {
                ForwardPropagation(x.Brightness);
                BackPropagation(x.Name);
                listOfErrors.Add(CalculateError(x.Name));

                if (cancellationToken.IsCancellationRequested)
                {
                    ExportNetwork.Export(this);
                    break;
                }
            }
            double error = listOfErrors.Average();

            return $"Error => {error}";
        }

        private void ForwardPropagation(params double[] entryValue)
        {
            var i = 0;
            for (int j = 0; j < EntryLayer.Neurons.Count; j++)
            {
                EntryLayer.Neurons[i].Value = entryValue[i++];
            }
            HiddenLayers.ForEach(x => x.Neurons.ForEach(y => y.CalculateEntry()));
            ExitLayer.Neurons.ForEach(x => x.CalculateEntry());
        }

        private void BackPropagation(params double[] goal)
        {
            var i = 0;
            ExitLayer.Neurons.ForEach(x => x.CalculateGradient(goal[i++]));
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
    }
}
