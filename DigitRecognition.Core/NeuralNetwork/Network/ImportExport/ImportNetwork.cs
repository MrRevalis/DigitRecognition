using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitRecognition.Core
{
    public class ImportNetwork
    {
        private static NetworkHelper GetNetwork(string path)
        {
            using (var file = File.OpenText(path))
            {
                return JsonConvert.DeserializeObject<NetworkHelper>(file.ReadToEnd());
            }
        }

        public static Network NetworkImport(string path)
        {
            var data = GetNetwork(path);
            if (data == null) return null;

            Network network = new Network(true);
            List<Neuron> neurons = new List<Neuron>();

            network.LearningRate = data.LearningRate;
            network.Momentum = data.Momentum;

            foreach(var x in data.EntryLayer)
            {
                Neuron neuron = new Neuron
                {
                    ID = x.ID,
                    Bias = x.Bias,
                    DeltaBias = x.DeltaBias,
                    Gradient = x.Gradient,
                    Value = x.Value
                };
                network.EntryLayer.Neurons.Add(neuron);
                neurons.Add(neuron);
            }

            foreach (var x in data.HiddenLayers)
            {
                Layer neuronsHidden = new Layer();
                foreach(var y in x)
                {
                    Neuron neuron = new Neuron
                    {
                        ID = y.ID,
                        Bias = y.Bias,
                        DeltaBias = y.DeltaBias,
                        Gradient = y.Gradient,
                        Value = y.Value
                    };
                    neuronsHidden.Neurons.Add(neuron);
                    neurons.Add(neuron);
                }
                network.HiddenLayers.Add(neuronsHidden);
            }

            foreach (var x in data.ExitLayer)
            {
                Neuron neuron = new Neuron
                {
                    ID = x.ID,
                    Bias = x.Bias,
                    DeltaBias = x.DeltaBias,
                    Gradient = x.Gradient,
                    Value = x.Value
                };
                network.ExitLayer.Neurons.Add(neuron);
                neurons.Add(neuron);
            }

            foreach(var x in data.Synapses)
            {
                Synapse synapse = new Synapse { ID = x.ID };

                Neuron entryNeuron = neurons.First(y => y.ID == x.EntryNeuronID);
                Neuron exitNeuron = neurons.First(y => y.ID == x.ExitNeuronID);

                synapse.EntryNeuron = entryNeuron;
                synapse.ExitNeuron = exitNeuron;
                synapse.Weight = x.Weight;
                synapse.DeltaWeight = x.DeltaWeight;

                entryNeuron.Exit.Add(synapse);
                exitNeuron.Entry.Add(synapse);
            }
            return network;
        }
    }
}
