using System;
namespace MLTrainingController.Models
{
    public class TrainingSet
    {
        public string[] products { get; set; }
        public string[] classes { get; set; }
        public int training_type {get; set;}
    }
}

