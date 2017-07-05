using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronal_Net_Test.NeuralNet.Neurons
{
    class InputNeuron : Neuron
    {
        private float value = 0;
        public void Setvalue(float x)
        {
            this.value = x;
        }
        public override float GetValue()
        {
            return this.value;
        }
    }
}
