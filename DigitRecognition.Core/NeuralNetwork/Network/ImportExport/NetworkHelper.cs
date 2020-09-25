using System;
using System.Collections.Generic;

namespace DigitRecognition.Core
{
    public class NetworkHelper
    {
        public double LearningRate { get; set; }
        public double Momentum { get; set; }
        public List<HelperNeuron> EntryLayer { get; set; }
        public List<List<HelperNeuron>> HiddenLayers { get; set; }
        public List<HelperNeuron> ExitLayer { get; set; }
        public List<HelperSynapse> Synapses { get; set; }

        public NetworkHelper()
        {
            EntryLayer = new List<HelperNeuron>();
            HiddenLayers = new List<List<HelperNeuron>>();
            ExitLayer = new List<HelperNeuron>();
            Synapses = new List<HelperSynapse>();
        }
    }

    public class HelperNeuron
    {
        public Guid ID { get; set; }
        public double Bias { get; set; }
        public double DeltaBias { get; set; }
        public double Gradient { get; set; }
        public double Value { get; set; }
    }

    public class HelperSynapse
    {
        public Guid ID { get; set; }
        public Guid EntryNeuronID { get; set; }
        public Guid ExitNeuronID { get; set; }
        public double Weight { get; set; }
        public double DeltaWeight { get; set; }
    }
}