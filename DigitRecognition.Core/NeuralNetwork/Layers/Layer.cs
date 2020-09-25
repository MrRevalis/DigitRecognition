using System.Collections.Generic;

namespace DigitRecognition.Core
{
    public class Layer
    {
        public List<Neuron> Neurons;

        public Layer()
        {
            Neurons = new List<Neuron>();
        }

        public Layer CreateLayer(int ammount)
        {
            Layer layer = new Layer();

            for (int i = 0; i < ammount; i++)
            {
                Neuron neuron = new Neuron();
                layer.Neurons.Add(neuron);
            }

            return layer;
        }
    }
}
