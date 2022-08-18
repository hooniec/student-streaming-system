﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreamTec.Models;
using Stream = StreamTec.Models.Stream;

namespace StreamTec.Controllers
{
    public class StreamController : Controller
    {
        public StreamController(WelTecContext context)
        {
            _context = context;
        }

        private readonly WelTecContext _context;

        // Stream Dictionary<majorname, Dictionary<courseid, List<stream object>>>
        Dictionary<string, Dictionary<string, List<Stream>>> streamDic = new Dictionary<string, Dictionary<string, List<Stream>>>();

        // A Dictionary for major names and its code
        Dictionary<string, string> majorDic = new Dictionary<string, string>()
        {
            { "Cyber Security", "CS" },
            { "Data Science", "DS"},
            { "Interaction Design", "ID" },
            { "Networking and Infra", "NI" },
            { "Software Development", "SD" },
            { "Other", "IT"},
        };

        public IActionResult Index()
        {
            // Classify major, course id based on course ID string
            try
            {
                // Load stream data as queryable object
                var streamDB = _context.Streams.AsQueryable();
                
                foreach (KeyValuePair<string, string> major in majorDic)
                {
                    // Iterate over the majorDic to add majors as keys into streamDic
                    streamDic.Add(major.Key, new Dictionary<string, List<Stream>>());

                    // Iterate over the streams from database.table.Stream
                    foreach (var stream in streamDB)
                    {
                        // Get first 7 charaters to extract course ID
                        string courseId = stream.StreamID.Substring(0, 7);

                        if (stream.StreamID.Contains(major.Value))
                        {
                            bool courseExist = streamDic[major.Key].ContainsKey(courseId);

                            if (courseExist)
                            {
                                streamDic[major.Key][courseId].Add(stream);
                            }
                            else if (!courseExist)
                            {
                                streamDic[major.Key].Add(courseId, new List<Stream>());
                                streamDic[major.Key][courseId].Add(stream);
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {

            }

            //var serialized = JsonConvert.SerializeObject(streamDic);
            //return Content(serialized, "application/json");
            return View(streamDic);
        }
    }
}