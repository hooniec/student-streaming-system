using Microsoft.AspNetCore.Mvc;

namespace StreamTec.Controllers
{
    public class StreamController : Controller
    {
        public IActionResult Index()
        {
            // TimeTable Dictionary <majorname, a list of streams>
            Dictionary<string, List<Models.Stream>> streamDic = new Dictionary<string, List<Models.Stream>>();

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

            // A list of streams
            List<Models.Stream> streamList = new List<Models.Stream>();

            foreach (KeyValuePair<string, string> major in majorDic)
            {
                streamDic.Add(major.Key, streamList.Where(s => s.StreamID.StartsWith(major.Value)).ToList());
                streamList.Clear();
            }

            return View(streamDic);
            //return View(_context.Streams.ToList());
        }
    }
}
