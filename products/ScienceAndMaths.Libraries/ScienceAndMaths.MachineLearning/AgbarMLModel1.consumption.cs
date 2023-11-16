﻿// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace ScienceAndMaths_MachineLearning
{
    public partial class AgbarMLModel1
    {
        /// <summary>
        /// model input class for AgbarMLModel1.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(1)]
            [ColumnName(@"Postcode")]
            public string Postcode { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"Month")]
            public string Month { get; set; }

            [LoadColumn(4)]
            [ColumnName(@"Day_of_week")]
            public string Day_of_week { get; set; }

            [LoadColumn(5)]
            [ColumnName(@"Normalized_c")]
            public float Normalized_c { get; set; }

            [LoadColumn(8)]
            [ColumnName(@"Population_higher16")]
            public float Population_higher16 { get; set; }

            [LoadColumn(9)]
            [ColumnName(@"Population_highschool")]
            public float Population_highschool { get; set; }

            [LoadColumn(10)]
            [ColumnName(@"Housing_biggerSize_rate")]
            public float Housing_biggerSize_rate { get; set; }

            [LoadColumn(11)]
            [ColumnName(@"Reservoirs")]
            public float Reservoirs { get; set; }

            [LoadColumn(12)]
            [ColumnName(@"Incomes")]
            public float Incomes { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for AgbarMLModel1.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Postcode")]
            public float[] Postcode { get; set; }

            [ColumnName(@"Month")]
            public float[] Month { get; set; }

            [ColumnName(@"Day_of_week")]
            public float[] Day_of_week { get; set; }

            [ColumnName(@"Normalized_c")]
            public float Normalized_c { get; set; }

            [ColumnName(@"Population_higher16")]
            public float Population_higher16 { get; set; }

            [ColumnName(@"Population_highschool")]
            public float Population_highschool { get; set; }

            [ColumnName(@"Housing_biggerSize_rate")]
            public float Housing_biggerSize_rate { get; set; }

            [ColumnName(@"Reservoirs")]
            public float Reservoirs { get; set; }

            [ColumnName(@"Incomes")]
            public float Incomes { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"Score")]
            public float Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("AgbarMLModel1.mlnet");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }
    }
}
