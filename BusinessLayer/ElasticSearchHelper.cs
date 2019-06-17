namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Text;
    using Elasticsearch.Net;
    using Nest;
    using Newtonsoft.Json;

    /// <summary>
    /// The elastic search helper.
    /// </summary>
    public static class ElasticSearchHelper
    {
        /// <summary>The get elastic client.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The ElasticClient by index name.</returns>
        public static ElasticClient GetElasticClient(string index)
        {
            var nodeUri = new Uri(ConfigurationManager.AppSettings["Elasticsearch"]);
            var pool = new StaticConnectionPool(new List<Uri> { nodeUri });
            var settings = new ConnectionSettings(pool, con => new MyJsonNetSerializer(con))
                .DefaultIndex(ConfigurationManager.AppSettings["Elastic" + index + "Index"])
                .DisableDirectStreaming()
                .OnRequestCompleted(details =>
                {
                    Debug.WriteLine("### ElasticSearch REQUEST ###");
                    if (details.RequestBodyInBytes != null)
                        Debug.WriteLine(Encoding.UTF8.GetString(details.RequestBodyInBytes));
                    Debug.WriteLine("### ElasticSearch RESPONSE ###");
                    if (details.ResponseBodyInBytes != null)
                        Debug.WriteLine(Encoding.UTF8.GetString(details.ResponseBodyInBytes));
                })
                .PrettyJson();

            var elasticClient = new ElasticClient(settings);
            elasticClient.CreateIndexByName(ConfigurationManager.AppSettings["Elastic" + index + "Index"]);

            return elasticClient;
        }
    }

    /// <summary>
    /// The my json net serializer.
    /// </summary>
    public class MyJsonNetSerializer : JsonNetSerializer
    {
        /// <summary>
        /// Initializes a new instance of the MyJsonNetSerializer class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public MyJsonNetSerializer(IConnectionSettingsValues settings)
            : base(settings)
        {
        }

        /// <summary>
        /// The modify json serializer settings.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        protected override void ModifyJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
