using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication7.Controllers
{
    [RoutePrefix("api/values")]
    [EnableCors(origins: "*", headers:"*", methods:"*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<objTaco> Get()
        {
            using (Data.DataSet1TableAdapters.taTACOS ta = new Data.DataSet1TableAdapters.taTACOS())
            {
                Data.DataSet1.TACOSDataTable dt = new Data.DataSet1.TACOSDataTable();

                ta.Fill(dt);

                List<objTaco> lista = new List<objTaco>();

                foreach (DataRow row in dt.Rows)
                {
                    objTaco obj = new objTaco();

                    obj.id = Convert.ToInt32(row["id"]);
                    obj.nombre = row["nombre"].ToString().Trim();
                    obj.cantidad = Convert.ToInt32(row["cantidad"]);

                    lista.Add(obj);
                }
                return lista;              
            }
        }


        
        // GET api/values/GetTotal
        [HttpGet]
        [Route("GetTotal")]
        public int? GetTotal()
        {
            
            using (Data.DataSet1TableAdapters.QueriesTableAdapter ta = new Data.DataSet1TableAdapters.QueriesTableAdapter())
            {
                int? result = (int?)ta.obtenerTotal();
  
                return result;
            }
               
        }



        // GET api/values/5
        public List<objTaco> Get(int id)
        {
            using (Data.DataSet1TableAdapters.taTACOS ta = new Data.DataSet1TableAdapters.taTACOS())
            {
                Data.DataSet1.TACOSDataTable dt = new Data.DataSet1.TACOSDataTable();

                ta.FillBy(dt, id);

                List<objTaco> lista = new List<objTaco>();

                foreach (DataRow row in dt.Rows)
                {
                    objTaco obj = new objTaco();

                    obj.id = Convert.ToInt32(row["id"]);
                    obj.nombre = row["nombre"].ToString().Trim();
                    obj.cantidad = Convert.ToInt32(row["cantidad"]);

                    lista.Add(obj);
                }
                return lista;
            }

        }

        // POST api/values
        public string Post([FromBody] objTaco values)
        {
            using (Data.DataSet1TableAdapters.taTACOS ta = new Data.DataSet1TableAdapters.taTACOS())
            {
                Data.DataSet1.TACOSDataTable dt = new Data.DataSet1.TACOSDataTable();

                objTaco obj = new objTaco();
                obj.nombre = values.nombre;
                obj.cantidad = values.cantidad;

                int result = ta.InsertQuery(obj.nombre, obj.cantidad.ToString());

                if(result == 0)
                {
                    return "Ocurio un error al tratar de guardar su informacion";
                }
                else
                {
                    return "Datos insertados correctamente";
                }

            }
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] objTaco values)
        {
            using (Data.DataSet1TableAdapters.taTACOS ta = new Data.DataSet1TableAdapters.taTACOS())
            {
                Data.DataSet1.TACOSDataTable dt = new Data.DataSet1.TACOSDataTable();

                objTaco obj = new objTaco();
                obj.nombre = values.nombre;
                obj.cantidad = values.cantidad;

                int result = ta.UpdateQuery(obj.nombre, obj.cantidad.ToString(), id);

                if (result == 0)
                {
                    return "Ocurio un error al tratar de actualizar el registro";
                }
                else
                {
                    return "Registro actualizado con exito";
                }


            }
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            using (Data.DataSet1TableAdapters.taTACOS ta = new Data.DataSet1TableAdapters.taTACOS())
            {
                Data.DataSet1.TACOSDataTable dt = new Data.DataSet1.TACOSDataTable();

                int result = ta.DeleteQuery(id);

                if (result == 0)
                {
                    return "Ocurio un error al tratar de Eliminar el registro";
                }
                else
                {
                    return "Registro eliminado con exito";
                }

            }
        }
    }

    public class objTaco
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public int cantidad { get; set; }
        // otros campos que necesites
    }
}
