using System.Collections.Generic;

namespace Neuronal_Net_Test.NeuralNet.Neurons
{
    class WorkingNeuron : Neuron
    {
        private float? value = null;
        private List<Connection> connections = new List<Connection>();

        public void AddNeuronConnection(Neuron n, float weight)
        {
            AddNeuronConnection(new Connection(n, weight));
        }
        private void AddNeuronConnection(Connection connection)
        {
            connections.Add(connection);
        }
        public void Train(float[] input, int result)
        {
            float alpha = 0.01F;
            Invalidate();
            Calculate();
            if (this.value - result >= 0.1)
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    connections[i].weight = connections[i].weight * alpha * input[i] * (result - this.value??0);
                }
            }
        }
        public void Invalidate()
        {
            this.value = null;
        }

        private void Calculate()
        {
            float value = 0;
            foreach (var c in connections)
            {
                value += c.GetValue();
            }
            value = Neuron.Sigmoid(value);
            this.value = value;
        }

        public override float GetValue()
        {
            if (value == null)
            {
                Calculate();
            }
            return (float)value;
        }
    }
}
