using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DigitRecognition.Core
{
    public class ExportNetwork
    {
        public static void Export(Network network)
        {
            var data = CreateExportNetwork(network);
            using (var file = File.CreateText("newNetwork.json"))
            {
                var serialize = new JsonSerializer { Formatting = Formatting.Indented };
                serialize.Serialize(file, data);
            }
        }

        private static NetworkHelper CreateExportNetwork(Network network)
        {
            NetworkHelper data = new NetworkHelper
            {
                LearningRate = network.LearningRate,
                Momentum = network.Momentum
            };

            foreach(var x in network.EntryLayer.Neurons)
            {
                var neuron = new HelperNeuron
                {
                    ID = x.ID,
                    Bias = x.Bias,
                    DeltaBias = x.DeltaBias,
                    Gradient = x.Gradient,
                    Value = x.Value
                };
                data.EntryLayer.Add(neuron);

                foreach(var y in x.Exit)
                {
                    var synapse = new HelperSynapse
                    {
                        ID = y.ID,
                        ExitNeuronID = y.ExitNeuron.ID,
                        EntryNeuronID = y.EntryNeuron.ID,
                        Weight = y.Weight,
                        DeltaWeight = y.DeltaWeight
                    };
                    data.Synapses.Add(synapse);
                }
            }

            foreach(var x in network.HiddenLayers)
            {
                var layer = new List<HelperNeuron>();

                foreach(var y in x.Neurons)
                {
                    var neuron = new HelperNeuron
                    {
                        ID = y.ID,
                        Bias = y.Bias,
                        DeltaBias = y.DeltaBias,
                        Gradient = y.Gradient,
                        Value = y.Value
                    };
                    layer.Add(neuron);

                    foreach(var z in y.Exit)
                    {
                        var synapse = new HelperSynapse
                        {
                            ID = z.ID,
                            ExitNeuronID = z.ExitNeuron.ID,
                            EntryNeuronID = z.EntryNeuron.ID,
                            Weight = z.Weight,
                            DeltaWeight = z.DeltaWeight
                        };
                        data.Synapses.Add(synapse);
                    }
                }
                data.HiddenLayers.Add(layer);
            }

            foreach(var x in network.ExitLayer.Neurons)
            {
                var neuron = new HelperNeuron
                {
                    ID = x.ID,
                    Bias = x.Bias,
                    DeltaBias = x.DeltaBias,
                    Gradient = x.Gradient,
                    Value = x.Value
                };
                data.ExitLayer.Add(neuron);

                foreach(var y in x.Exit)
                {
                    var synapse = new HelperSynapse
                    {
                        ID = y.ID,
                        ExitNeuronID = y.ExitNeuron.ID,
                        EntryNeuronID = y.EntryNeuron.ID,
                        Weight = y.Weight,
                        DeltaWeight = y.DeltaWeight
                    };
                    data.Synapses.Add(synapse);
                }
            }
            return data;
        }
    }
}
