using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronal_Net_Test.NeuralNet.Neurons
{
    class Connection
    {
        public float weight = 1;
        public Neuron entrieNeuron;

        public Connection(Neuron n, float weight)
        {
            this.weight = weight;
            this.entrieNeuron = n;
        }

        public float GetValue()
        {
            return weight * entrieNeuron.GetValue();
        }
    }
}
