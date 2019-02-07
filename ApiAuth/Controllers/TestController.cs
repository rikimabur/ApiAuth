using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    class TestController: ApiController
    {
        public string Get()
        {
            return string.Empty;
        }

        public string Get(int id)
        {
            return string.Empty;
        }

        public string GetAll()
        {
            return string.Empty;
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
