using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Neuronal_Net_Test.Models;
using Neuronal_Net_Test.NeuralNet;
using Neuronal_Net_Test.NeuralNet.Neurons;
using System;
using System.Text;

namespace Neuronal_Net_Test
{
    public partial class Form1 : Form
    {
        private Point? _lastPoint;
        private List<Point?> _points = new List<Point?>();
        private Graphics _gp1;
        private Graphics _gp2;
        private List<Partition> _partitions;
        private int _mostMatch = 0;
        private NeuralNetwork net = new NeuralNetwork();
        private List<char> values = new List<char>
        {
            'a', 'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','x','y','z'
        };

        public Form1()
        {
            InitializeComponent();
            _gp1 = panel1.CreateGraphics();
            _gp2 = panel2.CreateGraphics();
            SetPartitions(20, 24);
            for (int i = 0; i < 20*24; i++)
            {
                net.AddInputNeuron(new InputNeuron());
            }
            var rand = new Random();
            for (int i = 0; i < 20 * 24; i++)
            {
                net.AddHiddenNeuron(new WorkingNeuron());
            }
            for (int i = 0; i < 52; i++)
            {
                net.AddOutputNeuron(new WorkingNeuron());
            }
            net.GenerateFullMesh();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _points.Add(_lastPoint);
            _lastPoint = null;
            simplify_Panel1();
            var data = new List<float>();
            foreach (var part in _partitions)
            {
                data.Add((float)part.IncludesPoint / (float)_mostMatch);
            }
            net.CalculateValue(data);
            var max = net.GetMaxValue();
            label2.Text = values[max].ToString();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_lastPoint != null)
            {
                _points.Add(_lastPoint);
                var newPoint = new Point(e.X, e.Y);
                if (_lastPoint != null)
                {
                    _gp1.DrawLine(new Pen(Color.Black), (Point)_lastPoint, newPoint);
                }
                _lastPoint = newPoint;
            }
        }

        private void simplify_Panel1()
        {
            FindPointsIncluded();
            DrawRectangles();
        }

        private void SetPartitions(int xPartitions, int yPartitions)
        {
            _partitions = new List<Partition>();
            _points = new List<Point?>();
            var xL = panel1.Width / xPartitions;
            var yL = panel1.Height / yPartitions;
            for (var dY = 0; dY < yPartitions; dY++)
            {
                for (var dX = 0; dX < xPartitions; dX++)
                {
                    _partitions.Add(new Partition
                    {
                        Part = new Rectangle(dX * xL, dY * yL, xL, yL)
                    });
                }
            }
        }

        private void FindPointsIncluded()
        {
            foreach (var partition in _partitions)
            {
                foreach (var point in _points)
                {
                    if (partition.Part.Contains(point??new Point()))
                    {
                        partition.IncludesPoint += 1;
                    }
                }
                if (partition.IncludesPoint > _mostMatch)
                {
                    _mostMatch = partition.IncludesPoint;
                }
            }
            _points = new List<Point?>();
        }

        private void DrawRectangles()
        {
            foreach (var partition in _partitions)
            {
                _gp2.FillRectangle( new SolidBrush(Color.FromArgb(Convert.ToInt32(((float)partition.IncludesPoint / (float)_mostMatch * 255)),0,0,0)), partition.Part);
            }
        }

        private void RePantRectangles()
        {
            foreach (var partition in _partitions)
            {
                _gp2.FillRectangle(new SolidBrush(Color.White), partition.Part);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = textBox1.Text;
            var code = Encoding.ASCII.GetBytes(input)[0];
            if (code > 96)
            {
                code -= 96;
            }
            else
            {
                code -= 38;
            }
            var data = new float[20 * 24];
            for (var i = 0;i < _partitions.Count; i++)
            {
                data[i] = ((float)_partitions[i].IncludesPoint / (float)_mostMatch);
            }
            net.TrainNeuralNet(data, code);
            SetPartitions(20, 24);
            RePantRectangles();
        }
    }
}
