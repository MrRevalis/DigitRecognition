using System;

namespace DigitRecognition.Core
{
    public class Synapse
    {
        public Guid ID { get; set; }
        public Neuron EntryNeuron { get; set; }
        public Neuron ExitNeuron { get; set; }
        public double Weight { get; set; }
        public double DeltaWeight { get; set; }

        public Synapse() { }

        public Synapse(Neuron entry, Neuron exit)
        {
            ID = Guid.NewGuid();
            EntryNeuron = entry;
            ExitNeuron = exit;
            Weight = RandomClass.RandomWeight();
        }
    }
}
