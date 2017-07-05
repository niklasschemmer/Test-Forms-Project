﻿using Neuronal_Net_Test.NeuralNet.Neurons;
using System;
using System.Collections.Generic;

namespace Neuronal_Net_Test.NeuralNet
{
    class NeuralNetwork
    {
        private List<InputNeuron> inputNeurons = new List<InputNeuron>();
        private List<WorkingNeuron> hiddenNeurons = new List<WorkingNeuron>();
        private List<WorkingNeuron> outputNeurons = new List<WorkingNeuron>();

        public void CalculateValue(List<float> data)
        {
            if (data.Count == inputNeurons.Count)
            {
                for (int i = 0; i < Math.Min(inputNeurons.Count,data.Count); i++)
                {
                    inputNeurons[i].Setvalue(data[i]);
                }
            }
            CalculateValues();
        }

        private void CalculateValues()
        {
            foreach (var neuron in hiddenNeurons)
            {
                neuron.GetValue();
            }
            foreach (var neuron in outputNeurons)
            {
                neuron.GetValue();
            }
        }

        public void AddInputNeuron(InputNeuron neuron)
        {
            inputNeurons.Add(neuron);
        }
        public void AddHiddenNeuron(WorkingNeuron neuron)
        {
            hiddenNeurons.Add(neuron);
        }
        public void AddOutputNeuron(WorkingNeuron neuron)
        {
            outputNeurons.Add(neuron);
        }
        public void GenerateHiddenNeurons(int amount)
        {
            var rand = new Random();
            for (int i = 0; i < amount; i++)
            {
                hiddenNeurons.Add(new WorkingNeuron());
            }
        }
        public void GenerateFullMesh()
        {
            var rand = new Random();
            foreach (var wn in hiddenNeurons)
            {
                foreach (var input in inputNeurons)
                {
                    wn.AddNeuronConnection(input, 1);
                }
            }

            foreach (var wn in outputNeurons)
            {
                foreach (var wn2 in hiddenNeurons)
                {
                    wn.AddNeuronConnection(wn2, 1);
                }
            }
        }
        public void Invalidate()
        {
            foreach (var wn in hiddenNeurons)
            {
                wn.Invalidate();
            }
            foreach (var wn in outputNeurons)
            {
                wn.Invalidate();
            }
        }
    }
}
