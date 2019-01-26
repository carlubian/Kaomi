using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaomi.Core;
using Microsoft.AspNetCore.Mvc;

namespace Kaomi.WebAPI.Controllers
{
    [Route("Kaomi")]
    [ApiController]
    public class KaomiController : ControllerBase
    {
        // GET Kaomi
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Kaomi.WebAPI appears to be working.";
        }

        // GET Kaomi/PullFromUri
        [HttpGet("PullFromUri")]
        public ActionResult<string> PullFromUri(string asmName, string uri)
        {
            try
            {
                KaomiLoader.PullFromUri(asmName, new Uri(uri));
                return $"Assembly {asmName} pulled from requested URI.";
            }
            catch (Exception e)
            {
                return $"Error pulling assembly: {e.Message}";
            }
        }

        // GET Kaomi/Load
        [HttpGet("Load")]
        public ActionResult<string> Load(string path)
        {
            try
            {
                return KaomiLoader.Load(path);
            }
            catch (Exception e)
            {
                return $"Error loading assembly: {e.Message}";
            }
        }

        // GET Kaomi/List
        [HttpGet("List")]
        public ActionResult<string> List()
        {
            try
            {
                return KaomiLoader.List().Aggregate("", (str, elem) => $"{str};{elem}");
            }
            catch (Exception e)
            {
                return $"Error listing loaded assemblies: {e.Message}";
            }
        }

        // GET Kaomi/Unload
        [HttpGet("Unload")]
        public ActionResult<string> Unload(string path)
        {
            try
            {
                KaomiLoader.Unload(path);
                return $"Assembly {path} removed from memory.";
            }
            catch (Exception e)
            {
                return $"Error unloading assembly: {e.Message}";
            }
        }

        // GET Kaomi/InstanceProcess
        [HttpGet("InstanceProcess")]
        public ActionResult<string> InstanceProcess(string id, string type)
        {
            try
            {
                KaomiLoader.InstanceProcess(id, type);
                return "Process executed; Output should be visible on the Kestrel console window.";
            }
            catch (Exception e)
            {
                return $"Error instancing process: {e.Message}";
            }
        }

        // GET Kaomi/ListProcesses
        [HttpGet("ListProcesses")]
        public ActionResult<IEnumerable<string>> ListProcesses(string id, string type)
        {
            try
            {
                return KaomiLoader.ListProcesses().ToArray();
            }
            catch (Exception e)
            {
                return new[] { $"Error listing processes: {e.Message}" };
            }
        }

        // GET Kaomi/HasResults
        [HttpGet("HasResults")]
        public ActionResult<string> HasResults(string process)
        {
            try
            {
                return KaomiLoader.HasResults(process).ToString();
            }
            catch (Exception e)
            {
                return $"Error finding results: {e.Message}";
            }
        }

        // GET Kaomi/GetResults
        [HttpGet("GetResults")]
        public ActionResult<IEnumerable<string>> GetResults(string process)
        {
            try
            {
                return KaomiLoader.GetResults(process).ToArray();
            }
            catch (Exception e)
            {
                return new[] { $"Error returning results: {e.Message}" };
            }
        }
    }
}
