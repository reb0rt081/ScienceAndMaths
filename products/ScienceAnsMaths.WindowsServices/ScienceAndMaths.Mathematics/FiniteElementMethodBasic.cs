﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Mathematics
{
    public class FiniteElementMethodBasic
    {
        /// <summary>
        /// For 1D problems this represents a 1D element where we want to find a u(x) in a discrete set of elements (u1, u2)
        /// </summary>
        public class LinearElement
        {
            public LinearElement(double x1, double x2)
            {
                Vertex1 = new Node(x1, 0.0);

                Vertex2 = new Node(x2, 0.0);
            }

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
            /// The resultand matrix when the interpolation function is derived
            /// du/dx = 1/|A| * [1  1] * [u1    u2] -> du/dx = B * [u1 u2]
            /// </summary>
            /// <returns></returns>
            public double[][] GetBMatrix()
            {
                var matrix = MatrixOperations.MatrixCreate(1, 2);
                double determinant = GetInterpolationMatrixDeterminant();

                matrix[0][0] = - 1.0 / determinant;
                matrix[0][1] = 1.0 / determinant;
                
                return matrix;
            }
        }

        public class TriangularElement
        {
            public TriangularElement(double x1, double y1, double x2, double y2, double x3, double y3)
            {
                Vertex1 = new Node(x1, y1);

                Vertex2 = new Node(x2, y2);

                Vertex3 = new Node(x3, y3);
            }

            public Node Vertex1;

            public Node Vertex2;

            public Node Vertex3;

            //  Linear interpolation
            public double[][] GetInterpolationMatrix()
            {
                var matrix = MatrixOperations.MatrixCreate(3, 3);

                matrix[0][0] = 1;
                matrix[0][1] = Vertex1.X;
                matrix[0][2] = Vertex1.Y;

                matrix[1][0] = 1;
                matrix[1][1] = Vertex2.X;
                matrix[1][2] = Vertex2.Y;

                matrix[2][0] = 1;
                matrix[2][1] = Vertex3.X;
                matrix[2][2] = Vertex3.Y;

                return matrix;
            }

            public double GetInterpolationMatrixDeterminant()
            {
                var matrix = GetInterpolationMatrix();

                return (matrix[0][0] * matrix[1][1] * matrix[2][2] + matrix[0][1] * matrix[1][2] * matrix[2][0] +
                        matrix[1][0] * matrix[2][1] * matrix[0][2]) -
                       (matrix[0][2] * matrix[1][1] * matrix[2][0] + matrix[1][0] * matrix[0][1] * matrix[2][2] +
                        matrix[2][1] * matrix[1][2] * matrix[0][0]);
            }

            //  This matrix applies for a 
            public double[][] GetBMatrix()
            {
                var matrix = MatrixOperations.MatrixCreate(3, 6);
                double determinant = GetInterpolationMatrixDeterminant();

                matrix[0][0] = (Vertex2.Y - Vertex3.Y) / determinant;
                matrix[0][1] = 0.0;
                matrix[0][2] = (Vertex3.Y - Vertex1.Y) / determinant;
                matrix[0][3] = 0.0;
                matrix[0][4] = (Vertex1.Y - Vertex2.Y) / determinant;
                matrix[0][5] = 0.0;

                matrix[1][0] = 0.0;
                matrix[1][1] = -(Vertex2.X - Vertex3.X) / determinant;
                matrix[1][2] = 0.0;
                matrix[1][3] = -(Vertex3.X - Vertex1.X) / determinant;
                matrix[1][4] = 0.0;
                matrix[1][5] = -(Vertex1.X - Vertex2.X) / determinant;

                matrix[2][0] = -(Vertex2.X - Vertex3.X) / determinant;
                matrix[2][1] = (Vertex2.Y - Vertex3.Y) / determinant;
                matrix[2][2] = -(Vertex3.X - Vertex1.X) / determinant;
                matrix[2][3] = (Vertex3.Y - Vertex1.Y) / determinant;
                matrix[2][4] = -(Vertex1.X - Vertex2.X) / determinant;
                matrix[2][5] = (Vertex1.Y - Vertex2.Y) / determinant;

                return matrix;
            }
        }

        public class Node
        {
            public Node(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X;
            public double Y;
        }
    }
}
