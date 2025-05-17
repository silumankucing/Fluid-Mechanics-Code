using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Windows.Forms;

namespace SimpleCFD
{
    class Program
    {
        static void Main(string[] args)
        {
            int nPanels = 50;
            double chordLength = 1.0;
            double angleOfAttack = 5.0 * Math.PI / 180.0;

            var airfoil = GenerateNACA4DigitAirfoil(0, 0, 12, nPanels, chordLength);

            var pressureDistribution = SolvePanelMethod(airfoil, angleOfAttack);

            PlotPressureDistribution(pressureDistribution);
        }

        static List<(double x, double y)> GenerateNACA4DigitAirfoil(int m, int p, int t, int nPanels, double chordLength)
        {
            var points = new List<(double x, double y)>();
            double[] x = new double[nPanels + 1];
            double[] yt = new double[nPanels + 1];

            for (int i = 0; i <= nPanels; i++)
            {
                double theta = Math.PI * i / nPanels;
                x[i] = chordLength * (1 - Math.Cos(theta)) / 2;
                yt[i] = 5 * t * (0.2969 * Math.Sqrt(x[i] / chordLength) - 0.1260 * (x[i] / chordLength) -
                    0.3516 * Math.Pow(x[i] / chordLength, 2) + 0.2843 * Math.Pow(x[i] / chordLength, 3) -
                    0.1015 * Math.Pow(x[i] / chordLength, 4));
                points.Add((x[i], yt[i]));
            }

            return points;
        }

        static List<(double x, double cp)> SolvePanelMethod(List<(double x, double y)> airfoil, double angleOfAttack)
        {
            var pressureDistribution = new List<(double x, double cp)>();
            int nPanels = airfoil.Count - 1;

            for (int i = 0; i < nPanels; i++)
            {
                double x = (airfoil[i].x + airfoil[i + 1].x) / 2;
                double cp = 1 - Math.Pow(Math.Sin(angleOfAttack), 2);
                pressureDistribution.Add((x, cp));
            }

            return pressureDistribution;
        }

        static void PlotPressureDistribution(List<(double x, double cp)> pressureDistribution)
        {
            var plotModel = new PlotModel { Title = "Pressure Distribution on Airfoil" };
            var series = new LineSeries { Title = "Cp" };

            foreach (var point in pressureDistribution)
            {
                series.Points.Add(new OxyPlot.DataPoint(point.x, point.cp));
            }

            plotModel.Series.Add(series);

            var plotView = new OxyPlot.WindowsForms.PlotView { Model = plotModel };
            var form = new System.Windows.Forms.Form
            {
                Text = "CFD Visualization",
                Width = 800,
                Height = 600
            };
            form.Controls.Add(plotView);
            plotView.Dock = System.Windows.Forms.DockStyle.Fill;
            System.Windows.Forms.Application.Run(form);
        }
    }
}