﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using conekta;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SCT_iCare;

using System.IO;
using System.Text;
using System.Globalization;

namespace SCT_iCare.Controllers.Recepcion
{
    public class RecepcionController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: Pacientes
        public ActionResult Index()
        {
            return View(db.Paciente.ToList());
        }

        public ActionResult NextDay()
        {
            return View(db.Paciente.ToList());
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(string nombre, string telefono, string email, string usuario, string sucursal, string cantidad, string pago,/* string tipoL, string tipoT, */ string referencia)
        {
            Paciente paciente1 = new Paciente();

            string canal = null;

            if (Convert.ToInt32(cantidad) == 1)
            {
                Paciente paciente = new Paciente();
                paciente.Nombre = nombre.ToUpper()/*.Normalize(System.Text.NormalizationForm.FormD).Replace(@"´¨", "")*/;
                paciente.Telefono = telefono;
                paciente.Email = email;

                //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                //Se obtiene el número del contador desde la base de datos
                int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                //Contadores por número de citas en cada sucursal
                string contador = "";
                if (num == null)
                {
                    contador = "100";
                }
                else if (num < 10)
                {
                    contador = "00" + Convert.ToString(num);
                }
                else if (num >= 10 && num < 100)
                {
                    contador = "0" + Convert.ToString(num);
                }

                //Se asigna el número de ID del doctor
                //if(Convert.ToInt32(doc) < 10)
                //{
                //    doc = "0" + doc;
                //}

                string mes = DateTime.Now.Month.ToString();

                if (Convert.ToInt32(mes) < 10)
                {
                    mes = "0" + mes;
                }

                //Se crea el número de Folio
                string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                if (ModelState.IsValid)
                {
                    db.Paciente.Add(paciente);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                Sucursales suc = db.Sucursales.Find(idSuc);

                suc.Contador = Convert.ToInt32(num);

                if (ModelState.IsValid)
                {
                    db.Entry(suc).State = EntityState.Modified;
                    db.SaveChanges();
                    //No retorna ya que sigue el proceso
                    //return RedirectToAction("Index");
                }

                var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                Cita cita = new Cita();

                cita.idPaciente = idPaciente;
                cita.FechaReferencia = DateTime.Now;
                cita.Sucursal = sucursal;
                cita.FechaCita = DateTime.Now;
                cita.Recepcionista = usuario;
                cita.EstatusPago = "Pagado";
                cita.Referencia = referencia;
                cita.Folio = numFolio;
                cita.Canal1 = "SITIO";
                cita.TipoPago = pago;

                if (pago != "Referencia Scotiabank")
                {
                    var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == referencia select t).FirstOrDefault();

                    if (tipoPago != null)
                    {
                        ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                        refe.EstatusReferencia = "LIBRE";
                        refe.idPaciente = idPaciente;

                        if (ModelState.IsValid)
                        {
                            db.Entry(refe).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    //var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == numero select t).FirstOrDefault();

                    var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == referencia select t).FirstOrDefault();

                    if(tipoPago != null)
                    {
                        ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                        refe.EstatusReferencia = "OCUPADO";

                        if (ModelState.IsValid)
                        {
                            db.Entry(refe).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    
                }

                

                //-------------------------------------------------------------
                if (ModelState.IsValid)
                {
                    db.Cita.Add(cita);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            else
            {
                //return View(detallesOrden);
                for (int n = 1; n <= Convert.ToInt32(cantidad); n++)
                {
                    Paciente paciente = new Paciente();

                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    paciente.Telefono = telefono;
                    paciente.Email = email;

                    //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                    string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                    //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                    //Se obtiene el número del contador desde la base de datos
                    int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                    //Contadores por número de citas en cada sucursal
                    string contador = "";
                    if (num == null)
                    {
                        contador = "100";
                    }
                    else if (num < 10)
                    {
                        contador = "00" + Convert.ToString(num);
                    }
                    else if (num >= 10 && num < 100)
                    {
                        contador = "0" + Convert.ToString(num);
                    }

                    //Se asigna el número de ID del doctor
                    //if (Convert.ToInt32(doc) < 10)
                    //{
                    //    doc = "0" + doc;
                    //}

                    string mes = DateTime.Now.Month.ToString();

                    if (Convert.ToInt32(mes) < 10)
                    {
                        mes = "0" + mes;
                    }

                    //Se crea el número de Folio
                    string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                    paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                    if (ModelState.IsValid)
                    {
                        db.Paciente.Add(paciente);
                        db.SaveChanges();
                        //return RedirectToAction("Index");
                    }

                    int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                    Sucursales suc = db.Sucursales.Find(idSuc);

                    suc.Contador = Convert.ToInt32(num);

                    if (ModelState.IsValid)
                    {
                        db.Entry(suc).State = EntityState.Modified;
                        db.SaveChanges();
                        //No retorna ya que sigue el proceso
                        //return RedirectToAction("Index");
                    }

                    var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                    Cita cita = new Cita();

                    cita.TipoPago = pago;

                    cita.idPaciente = idPaciente;
                    cita.FechaReferencia = DateTime.Now;
                    cita.Sucursal = sucursal;
                    cita.Recepcionista = usuario;
                    cita.EstatusPago = "Pagado";
                    cita.Folio = numFolio;
                    cita.Referencia = referencia;
                    cita.Canal1 = nombre.ToUpper();
                    cita.FechaCita = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        db.Cita.Add(cita);
                        db.SaveChanges();
                        //no regresa ya que se debe ver la pantalla de Orden
                        //return RedirectToAction("Index");
                    }
                }
            }

            return Redirect("Index"); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Orden(string nombre, string telefono, string email, string usuario, string sucursal,  /*string tipoL, string tipoT,*/ string cantidad)
        {
            GetApiKey();

            int cantidadDiferencia;
            int cantidadReal = Convert.ToInt32(cantidad);
            string mailSeteado = email;

            if(Convert.ToInt32(cantidad) > 3)
            {
                cantidadDiferencia = Convert.ToInt32(cantidad) - 3;
                cantidadReal = 3;
                mailSeteado = "referenciaoxxo@medicinagmi.mx";
            }

            int precio = Convert.ToInt32(cantidadReal) * 2842;


            Order order = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, mailSeteado, telefono) + @",
                      ""line_items"": [{
                      ""name"": " + ConvertirProductos1(sucursal) + @",
                      ""unit_price"": " + ConvertirProductos2(Convert.ToString(precio)) + @",
                      ""quantity"": 1
                      }]
                      }");

            order.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": " + ConvertirProductos2(Convert.ToString(precio)) + @"
                    }");

            var orden = new Order().find(order.id);

            var detallesOrden = new Order()
            {
                id = orden.id,
                customer_info = orden.customer_info,
                line_items = orden.line_items,
                amount = orden.amount,
                charges = orden.charges
            };

            var referenciaSB = (from r in db.ReferenciasSB where r.EstatusReferencia == "LIBRE" select r.ReferenciaSB).FirstOrDefault();
            ViewBag.ReferenciaSB = referenciaSB;

            ViewBag.Orden = order.id;
            ViewBag.Metodo = "OXXO";

            if (Convert.ToInt32(cantidad) == 1)
            {
                Paciente paciente = new Paciente();

                paciente.Nombre = nombre.ToUpper();
                paciente.Telefono = telefono;
                paciente.Email = email;

                //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                //Se obtiene el número del contador desde la base de datos
                int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                //Contadores por número de citas en cada sucursal
                string contador = "";
                if (num == null)
                {
                    contador = "100";
                }
                else if (num < 10)
                {
                    contador = "00" + Convert.ToString(num);
                }
                else if (num >= 10 && num < 100)
                {
                    contador = "0" + Convert.ToString(num);
                }

                //Se asigna el número de ID del doctor
                //if (Convert.ToInt32(doc) < 10)
                //{
                //    doc = "0" + doc;
                //}

                string mes = DateTime.Now.Month.ToString();

                if (Convert.ToInt32(mes) < 10)
                {
                    mes = "0" + mes;
                }

                //Se crea el número de Folio
                string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                if (ModelState.IsValid)
                {
                    db.Paciente.Add(paciente);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                Sucursales suc = db.Sucursales.Find(idSuc);

                suc.Contador = Convert.ToInt32(num);

                if (ModelState.IsValid)
                {
                    db.Entry(suc).State = EntityState.Modified;
                    db.SaveChanges();
                    //No retorna ya que sigue el proceso
                    //return RedirectToAction("Index");
                }

                var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                Cita cita = new Cita();

                cita.TipoPago = "Referencia OXXO";
                cita.NoOrden = orden.id;
                

                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic datosCargo2 = js.Deserialize<dynamic>(orden.charges.data[0].ToString());

                string referencia = datosCargo2["payment_method"]["reference"].ToString();

                cita.Referencia = referencia;

                cita.idPaciente = idPaciente;
                cita.FechaReferencia = DateTime.Now;
                cita.Sucursal = sucursal;
                cita.Recepcionista = usuario;
                cita.EstatusPago = orden.payment_status;
                cita.Folio = numFolio;
                cita.Canal1 = "SITIO";
                cita.FechaCita = DateTime.Now;


                int idRefSB = Convert.ToInt32((from r in db.ReferenciasSB where r.ReferenciaSB == referenciaSB select r.idReferencia).FirstOrDefault());
                ReferenciasSB refe = db.ReferenciasSB.Find(idRefSB);
                refe.EstatusReferencia = "PENDIENTE";
                refe.idPaciente = idPaciente;

                if (ModelState.IsValid)
                {
                    db.Cita.Add(cita);
                    db.Entry(refe).State = EntityState.Modified;
                    db.SaveChanges();
                    //no regresa ya que se debe ver la pantalla de Orden
                    //return RedirectToAction("Index");
                }
            }
            else
            {
                //return View(detallesOrden);
                for (int n = 1; n <= Convert.ToInt32(cantidad); n++)
                {
                    Paciente paciente = new Paciente();

                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    paciente.Telefono = telefono;
                    paciente.Email = email;

                    //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                    string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                    //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                    //Se obtiene el número del contador desde la base de datos
                    int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                    //Contadores por número de citas en cada sucursal
                    string contador = "";
                    if (num == null)
                    {
                        contador = "100";
                    }
                    else if (num < 10)
                    {
                        contador = "00" + Convert.ToString(num);
                    }
                    else if (num >= 10 && num < 100)
                    {
                        contador = "0" + Convert.ToString(num);
                    }

                    //Se asigna el número de ID del doctor
                    //if (Convert.ToInt32(doc) < 10)
                    //{
                    //    doc = "0" + doc;
                    //}

                    string mes = DateTime.Now.Month.ToString();

                    if (Convert.ToInt32(mes) < 10)
                    {
                        mes = "0" + mes;
                    }

                    //Se crea el número de Folio
                    string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                    paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                    if (ModelState.IsValid)
                    {
                        db.Paciente.Add(paciente);
                        db.SaveChanges();
                        //return RedirectToAction("Index");
                    }

                    int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                    Sucursales suc = db.Sucursales.Find(idSuc);

                    suc.Contador = Convert.ToInt32(num);

                    if (ModelState.IsValid)
                    {
                        db.Entry(suc).State = EntityState.Modified;
                        db.SaveChanges();
                        //No retorna ya que sigue el proceso
                        //return RedirectToAction("Index");
                    }

                    var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                    Cita cita = new Cita();

                    cita.TipoPago = "Referencia OXXO";
                    cita.NoOrden = orden.id;
                 

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic datosCargo2 = js.Deserialize<dynamic>(orden.charges.data[0].ToString());

                    string referencia = datosCargo2["payment_method"]["reference"].ToString();

                    cita.Referencia = referencia;

                    cita.idPaciente = idPaciente;
                    cita.FechaReferencia = DateTime.Now;
                    cita.Sucursal = sucursal;
                    cita.Recepcionista = usuario;
                    cita.EstatusPago = orden.payment_status;
                    cita.Folio = numFolio;
                    cita.Canal1 = nombre.ToUpper();
                    cita.FechaCita = DateTime.Now;

                    int idRefSB = Convert.ToInt32((from r in db.ReferenciasSB where r.ReferenciaSB == referenciaSB select r.idReferencia).FirstOrDefault());
                    ReferenciasSB refe = db.ReferenciasSB.Find(idRefSB);
                    refe.EstatusReferencia = "PENDIENTE";
                    refe.idPaciente = idPaciente;

                    if (ModelState.IsValid)
                    {
                        db.Entry(refe).State = EntityState.Modified;
                        db.Cita.Add(cita);
                        db.SaveChanges();
                        //no regresa ya que se debe ver la pantalla de Orden
                        //return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.Cantidad = Convert.ToInt32(cantidad);
            return View(detallesOrden);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,Nombre,Telefono,Email,Folio")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarEstatus(int? id, string pago)
        {
            string referenciaNueva = "";
            if (pago != "Referencia Scotiabank")
            {
                //var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == numero select t).FirstOrDefault();

                var referenciaRepetida = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
                referenciaNueva = referenciaRepetida;
                var idFlag = (from i in db.Cita where i.Referencia == referenciaRepetida orderby i.idPaciente descending select i).FirstOrDefault();
                var tipoPago = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t).FirstOrDefault();

                if (tipoPago != null)
                {
                    ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                    refe.EstatusReferencia = "LIBRE";
                    refe.idPaciente = 0;

                    if(ModelState.IsValid)
                    {
                        db.Entry(refe).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                //var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == numero select t).FirstOrDefault();

                var referenciaRepetida = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
                var idFlag = (from i in db.Cita where i.Referencia == referenciaRepetida orderby i.idPaciente descending select i).FirstOrDefault();
                referenciaNueva = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t.ReferenciaSB).FirstOrDefault();
                var tipoPago = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t).FirstOrDefault();

                ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                refe.EstatusReferencia = "OCUPADO";

                if (ModelState.IsValid)
                {
                    db.Entry(refe).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var referencia = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
            var consulta = from c in db.Cita where c.Referencia == referencia select c;
            if (ModelState.IsValid)
            {
                foreach (var item in consulta)
                {
                    Cita cita = db.Cita.Find(item.idCita);

                    cita.EstatusPago = "Pagado";
                    cita.TipoPago = pago;
                    cita.Referencia = referenciaNueva;
                    db.Entry(cita).State = EntityState.Modified;
                    
                }
                db.SaveChanges();

            }
            
            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CURP_Expediente(string id, string nombre, string numero, string curp, string tel, string email)
        {
            int ide = Convert.ToInt32(id);

            string NOMBRE = null;
            string TELEFONO = null;
            string EMAIL = null;

            var paciente = (from p in db.Paciente where p.idPaciente == ide select p).FirstOrDefault();
            var cita = (from p in db.Cita where p.idPaciente == ide select p).FirstOrDefault();

            if(nombre == "")
            {
                NOMBRE = paciente.Nombre;
            }
            else
            {
                NOMBRE = nombre.ToUpper();
            }

            if (tel == "")
            {
                TELEFONO = paciente.Telefono;
            }
            else
            {
                TELEFONO = tel;
            }

            if(email == "")
            {
                EMAIL = paciente.Email;
            }
            else
            {
                EMAIL = email;
            }

            paciente.Nombre = NOMBRE.ToUpper();
            paciente.CURP = curp.ToUpper();
            paciente.Email = EMAIL;
            paciente.Telefono = TELEFONO;
            cita.NoExpediente = numero;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Digitalizar(HttpPostedFileBase file, string id, string usuario, string nombre, string doctor, string numero, string tipoL, string tipoT, string curp)
        {
            int ide = Convert.ToInt32(id);

            SCT_iCare.Expedientes exp = new SCT_iCare.Expedientes();
            var cita = (from c in db.Cita where c.idPaciente == ide select c).FirstOrDefault();
            var paciente = (from p in db.Paciente where p.idPaciente == ide select p).FirstOrDefault();
            Captura captura = new Captura();

            byte[] bytes2 = null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                bytes2 = bytes;

                //var bytesBinary = bytes;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=MyPDF.pdf");
                //Response.BinaryWrite(bytesBinary);
                //Response.End();
            }

            string CURP = null;
            string NOMBRE = null;
            string NOEXPEDIENTE = null;

            if (nombre == "")
            {
                NOMBRE = paciente.Nombre;
            }
            else
            {
                NOMBRE = nombre.ToUpper();
            }

            if (numero == "")
            {
                NOEXPEDIENTE = cita.NoExpediente;
            }
            else
            {
                NOEXPEDIENTE = numero;
            }

            if (curp == "")
            {
                CURP = paciente.CURP;
            }
            else
            {
                CURP = curp;
            }

            exp.Expediente = bytes2;
            exp.Recepcionista = usuario;
            exp.idPaciente = ide;
            paciente.Nombre = NOMBRE;
            paciente.CURP = CURP.ToUpper();
            captura.NombrePaciente = NOMBRE;
            captura.NoExpediente = NOEXPEDIENTE;
            captura.TipoTramite = tipoT;
            captura.EstatusCaptura = "No iniciado";
            captura.Doctor = doctor;
            captura.Sucursal = cita.Sucursal;
            captura.idPaciente = Convert.ToInt32(id);
            captura.FechaExpediente = DateTime.Now;

            Cita citaModificar = new Cita();
            cita.TipoLicencia = tipoL;
            cita.TipoTramite = tipoT;
            cita.NoExpediente = NOEXPEDIENTE;
            cita.Doctor = doctor;


            //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
            string SUC = (from S in db.Sucursales where S.Nombre == cita.Sucursal select S.SUC).FirstOrDefault();
           string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

            //Se obtiene el número del contador desde la base de datos del último registro de Folio incompleto
            //int? num = (from c in db.Sucursales where c.Nombre == cita.Sucursal select c.Contador).FirstOrDefault() + 1;
            var num = new string(cita.Folio.Reverse().Take(3).Reverse().ToArray());

            //Se asigna el número de ID del doctor
            if (Convert.ToInt32(doc) < 10)
            {
                doc = "0" + doc;
            }

            string mes = DateTime.Now.Month.ToString();

            if (Convert.ToInt32(mes) < 10)
            {
                mes = "0" + mes;
            }

            //Se crea el número de Folio
            string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + doc + num;
            cita.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + doc + num;
            paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + doc + num;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                //db.Cita.Add(cita);
                db.Expedientes.Add(exp);
                db.Captura.Add(captura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exp);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string ConvertirCliente(string nombre, string email, string telefono)
        {
            var newClient = new Customer()
            {
                name = nombre,
                email = email,
                phone = telefono
            };
            string jsonClient = JsonConvert.SerializeObject(newClient, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return jsonClient;
        }

        private string ConvertirProductos1(string consultorio)
        {
            string producto = "Consulta EPI (" + consultorio + ")";
            var product = new LineItem()
            {
                name = "Consulta EPI" + consultorio,
                //unit_price = 258800,
                //quantity = 1
            };

            string jsonProductos = JsonConvert.SerializeObject(producto, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return jsonProductos;
        }

        private int ConvertirProductos2(string precio)
        {
            int producto = Convert.ToInt32(precio) * 100;

            int jsonProductos = Convert.ToInt32(JsonConvert.SerializeObject(producto, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            return jsonProductos;
        }

        private long FechaExpira()
        {
            DateTime treintaDias = DateTime.Now.AddDays(2);
            long marcaTiempo = (Int64)(treintaDias.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            //string tiempo = marcaTiempo.ToString();
            return marcaTiempo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCompleto(HttpPostedFileBase file, string id, string nombre, string doctor, string numero, string tipoL, string tipoT
            , string pago, string telefono, string email, string referencia, string curp)
        {
            int ide = Convert.ToInt32(id);

            Paciente paciente = db.Paciente.Find(ide);
            var cita = (from c in db.Cita where c.idPaciente == ide select c).FirstOrDefault();
            var expediente = (from c in db.Expedientes where c.idPaciente == ide select c).FirstOrDefault();
            var captura = (from c in db.Captura where c.idPaciente == ide select c).FirstOrDefault();

            string ID = null;
            string NOMBRE = null;
            string DOCTOR = null;
            string NUMERO = null;
            string TIPOL = null;
            string TIPOT = null;
            string PAGO = null;
            string TELEFONO = null;
            string EMAIL = null;
            string REFERENCIA = null;
            string CURP = null;

            if(id == null)
            {
                ID = paciente.idPaciente.ToString();
            }
            else
            {
                ID = id;
            }

            if (nombre == "")
            {
                NOMBRE = paciente.Nombre;
            }
            else
            {
                NOMBRE = nombre;
            }

            if (doctor == "")
            {
                DOCTOR = cita.Doctor;
            }
            else
            {
                DOCTOR = doctor;
            }

            if (numero == "")
            {
                NUMERO = cita.NoExpediente;
            }
            else
            {
                NUMERO = numero;
            }

            if (tipoL == "")
            {
                TIPOL = cita.TipoLicencia;
            }
            else
            {
                TIPOL = tipoL;
            }

            if (tipoT == "")
            {
                TIPOT = cita.TipoTramite;
            }
            else
            {
                TIPOT = tipoT;
            }

            if (pago == "")
            {
                PAGO = cita.TipoPago;
            }
            else
            {
                PAGO = pago;
            }

            if (telefono == "")
            {
                TELEFONO = paciente.Telefono;
            }
            else
            {
                TELEFONO = telefono;
            }

            if (email == "")
            {
                EMAIL = paciente.Email;
            }
            else
            {
                EMAIL = email;
            }

            if (referencia == "")
            {
                REFERENCIA = cita.Referencia;
            }
            else
            {
                REFERENCIA = referencia;
            }

            if (curp == "")
            {
                CURP = paciente.CURP;
            }
            else
            {
                CURP = curp.ToUpper();
            }

            byte[] bytes2 = expediente.Expediente;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                bytes2 = bytes;

                //var bytesBinary = bytes;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=MyPDF.pdf");
                //Response.BinaryWrite(bytesBinary);
                //Response.End();
            }

            paciente.Nombre = NOMBRE;
            paciente.Telefono = TELEFONO;
            paciente.Email = EMAIL;
            paciente.CURP = CURP;

            cita.TipoPago = PAGO;
            cita.TipoLicencia = TIPOL;
            cita.NoExpediente = NUMERO;
            cita.TipoTramite = TIPOT;
            cita.Referencia = REFERENCIA;
            cita.Doctor = DOCTOR;

            expediente.Expediente = bytes2;

            captura.TipoTramite = TIPOT;
            captura.NombrePaciente = NOMBRE;
            captura.NoExpediente = NUMERO;
            captura.Doctor = DOCTOR;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                db.Entry(expediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Redirect("Index");
        }


        public JsonResult Buscar(string dato)
        {
            string parametro;

            if(dato.All(char.IsDigit))
            {
                parametro = dato;
            }
            else
            {
                parametro = dato.ToUpper();
            }

            List<Paciente> data = db.Paciente.ToList();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var selected = data.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m } )
                .Where(r => r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro)
                .Select(S => new { S.N.idPaciente, S.N.Nombre, S.N.Telefono, S.N.Email, S.N.Folio, S.N.CURP,
                    S.M.TipoPago, FechaCita = S.M.FechaCita.ToString(), S.M.NoOrden, S.M.EstatusPago, S.M.TipoLicencia, S.M.NoExpediente,
                    FechaReferencia  = S.M.FechaReferencia.ToString(), S.M.Referencia, S.M.Sucursal, S.M.Doctor, S.M.TipoTramite });

            //var joinSelected = selected.Join(db.Captura, n => n.idPaciente, d => d.idPaciente, (n, d) => new { N = n, D = d })
            //    .Select(S => new {
            //        S.N.idPaciente,
            //        S.N.Nombre,
            //        S.N.Telefono,
            //        S.N.Email,
            //        S.N.Folio,
            //        S.N.CURP,
            //        S.N.TipoPago,
            //        FechaCita = S.N.FechaCita.ToString(),
            //        S.N.NoOrden,
            //        S.N.EstatusPago,
            //        S.N.TipoLicencia,
            //        S.N.NoExpediente,
            //        FechaReferencia = S.N.FechaReferencia.ToString(),
            //        S.N.Referencia,
            //        S.N.Sucursal,
            //        S.N.Doctor,
            //        S.N.TipoTramite,
            //        S.D.EstatusCaptura
            //    }); ;

            //string nombre = selected.Nombre.ToString();

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarDictamen(string dato)
        {
            string parametro;

            if (dato.All(char.IsDigit))
            {
                parametro = dato;
            }
            else
            {
                parametro = dato.ToUpper();
            }

            List<Captura> data = db.Captura.ToList();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var selected = data.Where(r => r.NombrePaciente.Contains(parametro) || r.NoExpediente == parametro)
                .Select(S => new {
                    idCaptura = S.idCaptura,
                    S.NombrePaciente,
                    S.TipoTramite,
                    S.NoExpediente,
                    S.Sucursal,
                    S.Doctor,
                    S.EstatusCaptura
                }).FirstOrDefault();

            //string estatus = selected.EstatusCaptura;


            return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
}