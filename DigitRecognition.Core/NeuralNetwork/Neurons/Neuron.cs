using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitRecognition.Core
{
    public class Neuron
    {
        public Guid ID { get; set; }
        public List<Synapse> Entry { get; set; }
        public List<Synapse> Exit { get; set; }
        public double Bias { get; set; }
        public double DeltaBias { get; set; }
        public double Gradient { get; set; }
        public double Value { get; set; }

        public Neuron()
        {
            ID = Guid.NewGuid();
            Entry = new List<Synapse>();
            Exit = new List<Synapse>();
            Bias = RandomClass.RandomWeight();
        }

        public Neuron(IEnumerable<Neuron> entryNeuron) : this()
        {
            foreach (var x in entryNeuron)
            {
                var synapse = new Synapse(x, this);
                x.Exit.Add(synapse);
                Entry.Add(synapse);
            }
        }

        public Neuron(Layer entryNeurons) : this()
        {
            for (int i = 0; i < entryNeurons.Neurons.Count; i++)
            {
                var synapse = new Synapse(entryNeurons.Neurons[i], this);
                entryNeurons.Neurons[i].Exit.Add(synapse);
                Entry.Add(synapse);
            }
        }

        public virtual double CalculateEntry()
        {
            return Value = Sigmoid.CalculateSigmoid(Entry.Sum(x => x.Weight * x.EntryNeuron.Value) + Bias);
        }

        public double CalculateError(double x)
        {
            return x - Value;
        }

        public double CalculateGradient(double? x = null)
        {
            if (x == null)
            {
                return Gradient = Exit.Sum(y => y.ExitNeuron.Gradient * y.Weight) * Sigmoid.DerivativeSigmoid(Value);
            }
            return Gradient = CalculateError(x.Value) * Sigmoid.DerivativeSigmoid(Value);
        }

        public void UpdateWeight(double learningLevel, double momentum)
        {
            double lastDelta = DeltaBias;
            DeltaBias = learningLevel * Gradient;
            Bias += DeltaBias + momentum * lastDelta;

            foreach (var x in Entry)
            {
                lastDelta = x.DeltaWeight;
                x.DeltaWeight = learningLevel * Gradient * x.EntryNeuron.Value;
                x.Weight += x.DeltaWeight + momentum * lastDelta;
            }
        }
    }
}
