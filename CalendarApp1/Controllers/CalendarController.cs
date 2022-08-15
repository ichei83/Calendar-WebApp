using CalendarCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarServerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        MongoReposUtil _mongoRep;

        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
            _mongoRep = new MongoReposUtil();

        }

        //[HttpGet]
        //public IEnumerable<Calendar> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new Calendar
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet("GetAllCalendarMongoAsync")]
        public async Task<object> GetAllCalendarMongoAsync()
        {
            var CalendarData = new
            {
                Calendars = _mongoRep.GetAll<CalendarCommon.CalendarMongo>(x => true).ToList()
            };

            return CalendarData;
        }

        [HttpGet("GetNextEventId")]
        public object GetNextEventId()
        {
            string eventID = Guid.NewGuid().ToString();

            return  new { eventID = eventID };
        }

        [HttpPost("AddCalendar")]
        public MongoRepos.CalendarMongo AddCalendar([FromBody] MongoRepos.CalendarMongo Calendar)
        {
            //string eventID = Guid.NewGuid().ToString();
            //Calendar.eventId = eventID;
            return _mongoRep.Add(Calendar);
        }


        [HttpPost("UpdateCalendar")]

        public MongoRepos.CalendarMongo UpdateCalendar([FromBody] MongoRepos.CalendarMongo Calendar)
        {
            var newCal = Calendar;
            _mongoRep.Delete<MongoRepos.CalendarMongo>(o => o.eventId == Calendar.eventId);
            string eventID = Guid.NewGuid().ToString();
            newCal.eventId = eventID;
            return _mongoRep.Add(newCal);
            //return _mongoRep.Update(o => o.eventId == Calendar.eventId, Calendar);
            //return _mongoRep.Update1( Calendar);

        }

        //// DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _mongoRep.Delete<MongoRepos.CalendarMongo>(o => o.eventId == id);
        }

    }
}
