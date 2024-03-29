﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScienceAndMaths.MachineLearning.MistakeBoundModel;
using ScienceAndMaths.Shared.MachineLearning;

namespace ScienceAndMaths.MachineLearning.Test
{
    /// <summary>
    /// Test for mistake bound models
    /// </summary>
    [TestClass]
    public class MistakeBoundModelTest
    {
        private MistakeBoundDisjunctionModel mistakeBoundDisjunctionModel;
        private MistakeBoundDisjunctionExtensionModel mistakeBoundDisjunctionExtensionModel;
        private DecisionTreeModel decisionTreeModel;

        [TestInitialize]
        public void Initialize()
        {
            mistakeBoundDisjunctionModel = new MistakeBoundDisjunctionModel(2);
            mistakeBoundDisjunctionExtensionModel = new MistakeBoundDisjunctionExtensionModel(2);
            decisionTreeModel = new DecisionTreeModel(2);
        }

        [TestMethod]
        public void PredictDecisionTreeTest()
        {
            bool result = decisionTreeModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    false
                },
                Result = true
            });

            Assert.IsTrue(result);

            result = decisionTreeModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    false
                },
                Result = false
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ErrorRateDecisionTreeTest()
        {
            double errorRate = decisionTreeModel.ErrorRate(new List<MistakeBoundChallenge>
            {
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                }

            });

            Assert.AreEqual(25, errorRate);
        }

        /// <summary>
        /// In this example let us train the following decision tree as an example:
        ///         [Residente en españa? (X1)]
        ///           |             |
        ///           | FALSE       | TRUE
        ///         [FALSE]    [Ingresos > 20.000€ (X2)]
        ///                     |             |
        ///                     | FALSE       | TRUE
        ///                   [FALSE]       [TRUE]
        /// </summary>
        [TestMethod]
        public void TrainDecisionTreeTest()
        {
            var trainingSet = new List<MistakeBoundChallenge>
            {
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                }

            };
            decisionTreeModel.Train(trainingSet);

            Assert.AreEqual(0, decisionTreeModel.ErrorRate(trainingSet));
        }

        [TestMethod]
        public void GiniRateDecisionTreeTest()
        {
            var trainingSet = new List<MistakeBoundChallenge>
            {
                // 0 0 -> 0 (x1)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {   
                        false,
                        false
                    },
                    Result = false
                },
                // 0 0 -> 1 (x1)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        false
                    },
                    Result = true
                },
                // 0 1 -> 0 (x2)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                // 0 1 -> 1 (x1)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = true
                },
                // 1 0 -> 0 (x3)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = false
                },
                // 1 0 -> 1 (x1)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = true
                },
                // 1 1 -> 0 (x4)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = false
                },
                // 1 1 -> 1 (x2)
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                }


            };

            double index = decisionTreeModel.NegativeGiniRate(trainingSet, 0, null);
            Assert.IsTrue(Math.Abs(index - (double)4 / 9) < 0.00001);

            double indexX1 = decisionTreeModel.NegativeGiniRate(trainingSet, 0, decisionTreeModel.Concept[0]);
            Assert.AreEqual(0.44, indexX1);

            double indexX2 = decisionTreeModel.NegativeGiniRate(trainingSet, 0, decisionTreeModel.Concept[1]);
            Assert.IsTrue(Math.Abs(indexX2 - (double) 4/9) < 0.00001);
        }

        [TestMethod]
        public void PredictModelTest()
        {
            bool result = mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    false
                },
                Result = true
            });

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Prepares a training set to validate a monotone disjunction example that should be validated
        /// Test variables: {x1, x2}
        /// Expected result: {x1}, result does not depend on value of x2
        /// Test data:
        /// {0, 0} = 0
        /// {0, 1} = 0
        /// {1, 0} = 1
        /// {1, 1} = 1
        /// </summary>
        [TestMethod]
        public void TrainModelTest()
        {
            List<MistakeBoundChallenge> trainingSet = new List<MistakeBoundChallenge>
            {
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = true
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                }
            };

            mistakeBoundDisjunctionModel.Train(trainingSet);

            Assert.IsFalse(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    false
                },
                Result = true
            }));
            Assert.IsFalse(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    true
                },
                Result = true
            }));

            Assert.IsTrue(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    false
                },
                Result = false
            }));
            Assert.IsTrue(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    true
                },
                Result = false
            }));

            Assert.AreEqual(0, mistakeBoundDisjunctionModel.ErrorRate(trainingSet));
        }

        /// <summary>
        /// Prepares a training set to validate a monotone disjunction example that should be validated
        /// Test variables: {x1, x2}
        /// Expected result: {x1 || !x2}, result does not depend on value of x2
        /// Test data:
        /// {0, 0} = 1 = {0, 0, 1, 1}
        /// {0, 1} = 0 = {0, 1, 1, 0}
        /// {1, 0} = 1 = {1, 0, 0, 1}
        /// {1, 1} = 1 = {1, 1, 0, 0}
        /// </summary>
        [TestMethod]
        public void TrainExtendedModelTest()
        {
            List<MistakeBoundChallenge> trainingSet = new List<MistakeBoundChallenge>
            {
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        false
                    },
                    Result = true
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = true
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                }
            };

            mistakeBoundDisjunctionExtensionModel.Train(trainingSet);

            Assert.IsTrue(mistakeBoundDisjunctionExtensionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    false
                },
                Result = true
            }));
            Assert.IsFalse(mistakeBoundDisjunctionExtensionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    true
                },
                Result = true
            }));

            Assert.IsTrue(mistakeBoundDisjunctionExtensionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    false
                },
                Result = false
            }));
            Assert.IsTrue(mistakeBoundDisjunctionExtensionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    true
                },
                Result = true
            }));

            Assert.AreEqual(0, mistakeBoundDisjunctionExtensionModel.ErrorRate(trainingSet));
        }
    }
}
