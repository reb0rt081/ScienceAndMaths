﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Mathematics.FEM
{
    /// <summary>
    /// For 1D problems this represents a 1D element where we want to find a function u(x) in a discrete set of elements (u1, u2)
    /// </summary>
    public class LinearElement : IInterpolationElement
    {
        public LinearElement(double x1, double x2)
        {
            Vertex1 = new Node(x1);

            Vertex2 = new Node(x2);
        }

        public double SectionArea;

        public Node Vertex1;

        public Node Vertex2;

        /// <summary>
        /// Matrix "A" for a linear interpolation of the desired solution
        /// [1  x1]
        /// [1  x2]
        /// </summary>
        /// <returns></returns>
        public double[][] GetInterpolationMatrix()
        {
            var matrix = MatrixOperations.MatrixCreate(2, 2);

            matrix[0][0] = 1;
            matrix[0][1] = Vertex1.X;

            matrix[1][0] = 1;
            matrix[1][1] = Vertex2.X;

            return matrix;
        }

        public double GetInterpolationMatrixDeterminant()
        {
            var matrix = GetInterpolationMatrix();

            return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];
        }

        /// <summary>
        /// The resultant "B" matrix when the interpolation function is derived
        /// du/dx = 1/|A| * [1  1] * [u1    u2] -> du/dx = B * [u1 u2]
        /// </summary>
        /// <returns></returns>
        public double[][] GetBMatrix()
        {
            var matrix = MatrixOperations.MatrixCreate(1, 2);
            double determinant = GetInterpolationMatrixDeterminant();

            matrix[0][0] = -1.0 / determinant;
            matrix[0][1] = 1.0 / determinant;

            return matrix;
        }
    }
}