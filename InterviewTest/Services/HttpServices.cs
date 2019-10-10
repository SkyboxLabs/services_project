using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InterviewTest.Model;
using Newtonsoft.Json;

namespace InterviewTest.Services
{
    public class HttpServices
    {
        private TreeNodeCollection nodeCollection = new TreeNodeCollection();
        private Dictionary<Guid, int> scoreMap = new Dictionary<Guid, int>();

        /// <summary>
        /// Adds a score to the scoreboard.
        /// </summary>
        /// <param name="requestMessage">request</param>
        /// <returns>response</returns>
        public async Task<HttpResponseMessage> AddScore(HttpRequestMessage requestMessage)
        {
            var request = JsonConvert.DeserializeObject<AddScoreRequest>(await requestMessage.Content.ReadAsStringAsync());

            nodeCollection.Add(request.UserId, request.Score);
            scoreMap[request.UserId] = request.Score;

            return new HttpResponseMessage(HttpStatusCode.Created); ;
        }

        /// <summary>
        /// Gets the position of a given user in the scoreboard.
        /// </summary>
        /// <param name="requestMessage">request</param>
        /// <returns>response</returns>
        public async Task<HttpResponseMessage> GetPosition(HttpRequestMessage requestMessage)
        {
            var request = JsonConvert.DeserializeObject<GetPositionRequest>(await requestMessage.Content.ReadAsStringAsync());

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            var response = new GetPositionResponse
            {
                Position = this.nodeCollection.GetPosition(this.scoreMap[request.UserId])
            };

            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(response));
            return responseMessage;
        }
    }

    public class AddScoreRequest
    {
        public Guid UserId { get; set; }

        public int Score { get; set; }
    }

    public class AddScoreResponse
    {
        public int Position { get; set; }
    }

    public class GetPositionRequest
    {
        public Guid UserId { get; set; }
    }

    public class GetPositionResponse
    {
        public int Position { get; set; }
    }
}
