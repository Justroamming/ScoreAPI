/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CohortsController : ControllerBase
    {
        ScoreTableContext stc;
        public CohortsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Cohort/GetAllCohorts")]
        public IActionResult GetAllCohorts()
        {
            return Ok(new { data = stc.TblCohorts.ToList() });
        }

        [HttpGet]
        [Route("/Cohort/GetCohortById")]
        public IActionResult GetCohortById(string id)
        {
            return Ok(new { data = stc.TblCohorts.Find(new Guid(id)) });
        }

        [HttpPost]
        [Route("/Cohort/InsertCohort")]
        public IActionResult InsertCohort(string CName, string Description)
        {
            TblCohort co = new TblCohort();
            co.CohortId = System.Guid.NewGuid();
            co.CohortName = CName;
            co.Description=Description;

            stc.TblCohorts.Add(co);
            stc.SaveChanges();
            return Ok(new { co });

        }

        [HttpPut]
        [Route("/Cohort/UpdateCohort")]
        public IActionResult UpdateCohort(string id,string CName, string Description)
        {
            TblCohort co = new TblCohort();
            co.CohortId = new Guid(id);
            co.CohortName = CName;
            co.Description = Description;

            stc.TblCohorts.Update(co);
            stc.SaveChanges();
            return Ok(new { co });
        }

        [HttpDelete]
        [Route("/Cohort/DeleteCohort")]
        public IActionResult DeleteCohort(string id) { 
            TblCohort co =new TblCohort();
            co.CohortId = new Guid(id);

            stc.TblCohorts.Remove(co);
            stc.SaveChanges();

            return Ok(new { co });
        }
    }
}
*/