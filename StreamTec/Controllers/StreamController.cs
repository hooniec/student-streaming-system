using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreamTec.Models;
using Stream = StreamTec.Models.Stream;

namespace StreamTec.Controllers
{
    /// <summary>
    /// StreamController contains the methods to function streaming system
    /// </summary>
    [Authorize(Roles = "Student")]
    public class StreamController : Controller
    {
        /// <summary>
        /// Creating StreamController object with context.
        /// </summary>
        /// <param name="context">Database to manage data</param>
        public StreamController(WelTecContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Load the database context as _context
        /// </summary>
        private readonly WelTecContext _context;

        /// <summary>
        /// Load 
        /// </summary>
        /// <returns></returns>
        //public List<Stream> StreamList()
        //{
        //    var enrollments = _context.Streams.ToList();
        //    return (enrollments);
        //}

        // Stream Dictionary<majorname, Dictionary<courseid, List<stream object>>>
        Dictionary<string, Dictionary<string, List<Stream>>> streamDic = new Dictionary<string, Dictionary<string, List<Stream>>>();

        // A Dictionary for major names and its code
        Dictionary<string, string> majorDic = new Dictionary<string, string>()
        {
            { "Cyber Security", "CS" },
            { "Data Science", "DS"},
            { "Networking and Infra", "NI" },
            { "Software Development", "SD" },
            { "Other", "IT"},
        };

        /// <summary>
        /// Load Stream rows to classify its major and course name and put them into nested dictionary
        /// </summary>
        /// <returns>Nested dictionary with stream data</returns>
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
                            // if course ID is not exist in keys of dictionary, creating a new key using courseId
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

            return View(streamDic);
        }
    }
}
