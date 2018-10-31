using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
namespace Company.Function
{
    
    public static class eventREST
    {
        private static DB db = new DB();
        [FunctionName("Events/Get")]
        public static IActionResult GetEvent([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="events/")]HttpRequest req, TraceWriter log) {
            return (ActionResult) new OkObjectResult(db.GetData("events"));
        }
        /* public static IActionResult CancelPenalty([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "events/{event_id}/cancelPenalty")]HttpRequest req, string event_id, TraceWriter log)
        {
            var db = DB.GetConnection();
            var collection = db.GetCollection<BsonDocument>("members");



            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            String name = data?.name;
            var oneEvent = GetData(db.GetCollection<BsonDocument>("events"), new BsonDocument("_id" , new ObjectId(event_id)))[0];
            String penalty;
            // Determine penalties and display them
            if (!(Boolean) oneEvent["hourDeductions"])
            {
                penalty = "You will lose no hours by cancelling.";
            }
            else
            {
                var cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                switch ((DateTime.Parse(((string) oneEvent["date"]).Split('-')[0]).Add(new TimeSpan(23,59,59)) - TimeZoneInfo.ConvertTime(DateTime.UtcNow, cst)).Days)
                {
                    case 0:
                        penalty = "You will be losing all the hours you could've earned.";
                        break;
                    case 1:
                        penalty = "You will lose 2 hours.";
                        break;
                    case 2:
                        penalty = "You will lose 1 hour.";
                        break;
                    case int i when i >= 3:
                        penalty = "You will lose no hours by cancelling.";
                        break;
                    default:
                        penalty = "Please contact secretaries to cancel this event.";
                        break;

                }
            }
            return (ActionResult)new OkObjectResult($"{penalty} Are you sure you want to cancel?");
        }
        [FunctionName("CancelEvent")]
        public static IActionResult CancelEvent([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="events/{event_id}/cancel")]HttpRequest req, string event_id, TraceWriter log) {
            return (ActionResult)new OkObjectResult($"Are you sure you want to cancel?");
        }
        public static List<BsonDocument> GetData(IMongoCollection<BsonDocument> collection)
        {
            return collection.Find(new BsonDocument()).ToList();
        }
        public static List<BsonDocument> GetData(IMongoCollection<BsonDocument> collection, FilterDefinition<BsonDocument> filter)
        {
            return collection.Find(filter).ToList();
        } */
    }
}
