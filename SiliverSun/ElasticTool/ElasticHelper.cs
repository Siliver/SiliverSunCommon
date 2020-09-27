using Nest;
using SiliverSun.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliverSun.ElasticTool
{
    public class ElasticHelper
    {
        private IElasticClient _client;

        public ElasticHelper() {
            _client = ElasticClientSingle.elasticClient;
        }


        public void ElasticMachineLearn<T>()where T:class {
            //TestModel
            var putJobResponse = _client.MachineLearning.PutJob<T>("id",c=>c.Description("Lab 1 - Simple example").ResultsIndexName("server-metries").AnalysisConfig(a=>a.BucketSpan("30m").Latency("0s").Detectors(d=>d.Sum(c=>c.FieldName("test")))).DataDescription(d=>d.TimeField("timestamp")));
        }
    }
}
