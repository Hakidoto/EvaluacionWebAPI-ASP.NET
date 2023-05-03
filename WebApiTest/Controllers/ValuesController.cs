using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Cuestionario> Get()
        {
            using (db_cuestionarioEntities entities = new db_cuestionarioEntities())
            {
                return entities.Cuestionario.ToList();
            }
        }
    }
}
