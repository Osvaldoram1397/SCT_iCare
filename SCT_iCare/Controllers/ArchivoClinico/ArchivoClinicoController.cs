﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.ArchivoClinico
{
    public class ArchivoClinicoController : Controller
    {
        GMIEntities db = new GMIEntities();

        public ActionResult Index(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if(revision.InicioCarrusel == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.InicioCarrusel = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        
        public ActionResult Recepcion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SIGNOS_VITALES(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.SignosVitales == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult AUDIOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Audiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Audiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult OFTALMOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }



        [HttpPost]
        public ActionResult ODONTOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Odontologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult HISTORIA_CLÍNICA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.HistoriaClinca == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.HistoriaClinca = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return View();
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult CARDIOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Cardiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult LABORATORIO(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Laboratorio == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Laboratorio = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }


        public ActionResult NoPatologicos(int id)
        {
            ViewBag.idPaciente = id;
            return View();
        }

        public ActionResult Patologicos(int id)
        {
            ViewBag.idPaciente = id;
            return View();
        }

        public ActionResult AparatosSistemas(int id)
        {
            ViewBag.idPaciente = id;
            return View();
        }

        //Guardar módulos--------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Guardar_SignosVitales(int id, double sistolica, double diastolica, double cardiaca, double respiratoria, double temperatura, double peso, double estatura, double imc, double cintura, double cuello,  double grasa)
        {
            EPI_SignosVitales sv = new EPI_SignosVitales();
            sv.idPaciente = id;
            sv.IMC = imc.ToString();
            sv.Sistolica = sistolica.ToString();
            sv.Diastolica = diastolica.ToString();
            sv.Cardiaca = cardiaca.ToString();
            sv.Respiratoria = respiratoria.ToString();
            sv.Temperatura = temperatura.ToString();
            sv.Peso = peso.ToString();
            sv.Estatura = estatura.ToString();
            sv.Cintura = cintura.ToString();
            sv.Cuello = cuello.ToString();
            sv.Grasa = grasa.ToString();

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_SignosVitales == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_SignosVitales.Add(sv);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Audiologia(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Audiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Audiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        //[HttpPost]
        //public ActionResult Guardar_Oftalmologia(int id)
        //{
        //    var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

        //    if (revision.Fin_Oftalmologia == null)
        //    {
        //        var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
        //        cm.Fin_Oftalmologia = DateTime.Now;

        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(cm).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //    }

        //    ViewBag.idPaciente = id;
        //    return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        //}

        public ActionResult Guardar_Oftalmologia(int id, double izquierdoavc, double izquierdoavm, double izquierdoavl, double derechoavc, double derechoavm, double derechoavl,
        string agudezavisual, string estereopsis, string cartaaccidentes, string campovisual, string movimientosoculares, string derechorp, string izquierdorp,
        string cromatica)
        {

            Epi_Oftalmologia oftal = new Epi_Oftalmologia();
            oftal.IzquierdoAVC = izquierdoavc.ToString();
            oftal.IzquierdoAVM = izquierdoavm.ToString();
            oftal.IzquierdoAVL = izquierdoavl.ToString();
            oftal.DerechoAVC = derechoavc.ToString();
            oftal.DerechoAVM = derechoavm.ToString();
            oftal.DerechoAVL = derechoavl.ToString();
            oftal.AgudezaVisual = agudezavisual == "on" ? "SI" : "NO";
            oftal.Estereopsis = estereopsis == "on" ? "SI" : "NO"; ;
            oftal.CartaAccidentes = cartaaccidentes == "on" ? "SI" : "NO"; ;
            oftal.MovimientosOculares = movimientosoculares == "on" ? "SI" : "NO"; ;
            oftal.CampoVisual = campovisual == "on" ? "SI" : "NO"; ;
            oftal.DerechoRP = derechorp == "on" ? "SI" : "NO"; ;
            oftal.IzquierdoRP = izquierdorp == "on" ? "SI" : "NO"; ;
            oftal.Cromatica = cromatica;
            oftal.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Epi_Oftalmologia.Add(oftal);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Odontologia(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Odontologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Heredofamiliares(int id, string madrevive, string padrevive, string hermanosviven, string familiagrave, string diabetes, string arterial, string obesidad, string isquemica, string cerebral, string miocardio, string tiroides, string neoplastica)
        {
            EPI_A_Heredofamiliares heredo = new EPI_A_Heredofamiliares();

            heredo.MadreVive = madrevive == "on" ? "SI" : "NO";
            heredo.PadreVive = padrevive == "on" ? "SI" : "NO";
            heredo.HermanosViven = hermanosviven == "on" ? "SI" : "NO";
            heredo.FamiliaGrave = familiagrave == "on" ? "SI" : "NO";
            heredo.Diabetes = diabetes == "on" ? "SI" : "NO";
            heredo.Hipertension = arterial == "on" ? "SI" : "NO";
            heredo.Obesidad = obesidad == "on" ? "SI" : "NO";
            heredo.Cardiopatia = isquemica == "on" ? "SI" : "NO";
            heredo.VascularCerebral = cerebral == "on" ? "SI" : "NO";
            heredo.Infarto = miocardio == "on" ? "SI" : "NO";
            heredo.Tiroides = tiroides == "on" ? "SI" : "NO";
            heredo.Neoplasticas = neoplastica == "on" ? "SI" : "NO";
            heredo.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_A_Heredofamiliares.Add(heredo);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();

            if(revision1 != null && revision2 != null && revision3 != null && revision4 != null)
            {
                var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

                if (revision.Fin_HistoriaClinica == null)
                {
                    var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                    cm.Fin_HistoriaClinica = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        db.Entry(cm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                ViewBag.idPaciente = id;
                return RedirectToAction("Index", "ArchivoClinico", new { id = id });
            }

            if(revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_NoPatologicos(int id, string vacunas, string civil, string religion, string escolaridad, string hijos)
        {
            EPI_A_NoPatologicos nopat = new EPI_A_NoPatologicos();

            nopat.VacunasCompletas = vacunas == "on" ? "SI" : "NO";
            nopat.EstadoCivil = civil;
            nopat.Religion = religion;
            nopat.Escolaridad = escolaridad;
            nopat.Hijos = hijos;
            nopat.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_A_NoPatologicos.Add(nopat);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null)
            {
                var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

                if (revision.Fin_HistoriaClinica == null)
                {
                    var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                    cm.Fin_HistoriaClinica = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        db.Entry(cm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                ViewBag.idPaciente = id;
                return RedirectToAction("Index", "ArchivoClinico", new { id = id });
            }

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_AparatosSistemas(int id, string sintoma, string alteracion, string lentes, string cancer, string audicion,
            string auditivo, string nariz, string boca, string cardiaca, string respiratoria, string endocrino, string urinaria, string venas, 
            string intestinal, string articulaciones, string suenio, string apnea, string neurologica, string cuello, string abdomen, string extremidades)
        {
            EPI_AparatosSistemas apa = new EPI_AparatosSistemas();

            apa.Sintoma = sintoma == "on" ? "SI" : "NO";
            apa.AlteracionVista = alteracion == "on" ? "SI" : "NO";
            apa.Lentes = lentes; 
            apa.Audicion = audicion == "on" ? "SI" : "NO";
            apa.Auditivo = auditivo == "on" ? "SI" : "NO";
            apa.Nariz = nariz == "on" ? "SI" : "NO";
            apa.Gusto = boca == "on" ? "SI" : "NO";
            apa.Cardiaca = cardiaca == "on" ? "SI" : "NO";
            apa.Respiratoria = respiratoria == "on" ? "SI" : "NO";
            apa.Endocrinologica = endocrino == "on" ? "SI" : "NO";
            apa.Urinaria = urinaria == "on" ? "SI" : "NO";
            apa.Venas = venas == "on" ? "SI" : "NO";
            apa.Intestinal = intestinal == "on" ? "SI" : "NO";
            apa.Articulaciones = articulaciones == "on" ? "SI" : "NO";
            apa.Suenio = suenio == "on" ? "SI" : "NO";
            apa.Apnea = apnea == "on" ? "SI" : "NO";
            apa.Neurologica = neurologica == "on" ? "SI" : "NO";
            apa.CabezaCuello = cuello == "on" ? "SI" : "NO";
            apa.Abdomen = abdomen == "on" ? "SI" : "NO";
            apa.Extremidades = extremidades == "on" ? "SI" : "NO";
            apa.CancerSangre = cancer == "on" ? "SI" : "NO";
            apa.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_AparatosSistemas.Add(apa);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null)
            {
                var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

                if (revision.Fin_HistoriaClinica == null)
                {
                    var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                    cm.Fin_HistoriaClinica = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        db.Entry(cm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                ViewBag.idPaciente = id;
                return RedirectToAction("Index", "ArchivoClinico", new { id = id });
            }

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_Patologicos(int id, string congenita, string alergias, string memoria, string obstructiva, string quirurgicos, string transfuncionales,
            string diabetes, string convunsivas, string traumatismo, string oncologicos, string cardiopatias, string hipertension, string fuma, string alcoholicas,
            string drogas)
        {
            EPI_A_Patologicos patologicos = new EPI_A_Patologicos();

            patologicos.EnfermedadCongenita = congenita == "on" ? "SI" : "NO";
            patologicos.Alergias = alergias == "on" ? "SI" : "NO";
            patologicos.TranstornoMemoria = memoria == "on" ? "SI" : "NO";
            patologicos.Pulmonar = obstructiva == "on" ? "SI" : "NO";
            patologicos.Quirurgicos = quirurgicos == "on" ? "SI" : "NO";
            patologicos.Transfuncionales = transfuncionales == "on" ? "SI" : "NO";
            patologicos.Diabetes = diabetes == "on" ? "SI" : "NO";
            patologicos.Convulsivas = convunsivas == "on" ? "SI" : "NO";
            patologicos.Traumatismo = traumatismo == "on" ? "SI" : "NO";
            patologicos.Oncologicos = oncologicos == "on" ? "SI" : "NO";
            patologicos.Cardiopatias = cardiopatias == "on" ? "SI" : "NO";
            patologicos.Hipertension = hipertension == "on" ? "SI" : "NO";
            patologicos.Fuma = fuma == "on" ? "SI" : "NO";
            patologicos.Alcoholismo = alcoholicas == "on" ? "SI" : "NO";
            patologicos.Drogas = drogas == "on" ? "SI" : "NO";


            patologicos.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_A_Patologicos.Add(patologicos);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null)
            {
                var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

                if (revision.Fin_HistoriaClinica == null)
                {
                    var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                    cm.Fin_HistoriaClinica = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        db.Entry(cm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                ViewBag.idPaciente = id;
                return RedirectToAction("Index", "ArchivoClinico", new { id = id });
            }

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_Cardiologia(int id, string ritmo, double frecuencia, double eje, double pr, double qt, double qrs, double ondap, double ondaT)
        {
            EPI_Cardiologia cardio = new EPI_Cardiologia();

            cardio.Ritmo = ritmo;
            cardio.Frecuencia = frecuencia.ToString();
            cardio.Eje = eje.ToString();
            cardio.PR = pr.ToString();
            cardio.QT = qt.ToString();
            cardio.QRS = qrs.ToString();
            cardio.OndaP = ondap.ToString();
            cardio.OndaT = ondaT.ToString();
            cardio.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Cardiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_Cardiologia.Add(cardio);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Laboratorio(int id, int glucosa, int hemoglobina)
        {
            EPI_Laboratorio lab = new EPI_Laboratorio();

            lab.Glucosa = glucosa.ToString();
            lab.HemoglobinaGlucosilada = hemoglobina.ToString();
            lab.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Laboratorio == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Laboratorio = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_Laboratorio.Add(lab);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }
    }
}