using Apiders2.Entities;
using Apiders2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Apiders2.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)] // swaggerde gozukmeemesi için
    public class KategoriController : ApiController
    {

        public GenelModel getList()
        {
            GenelModel model = new GenelModel();

            try
            {
                POSDBEntities context = new POSDBEntities();
                List<Kategoriler> listem = context.Kategoriler.ToList();
                model.data = listem;
            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;
        }


        public GenelModel delete(int id)
        {
            GenelModel model = new GenelModel();
            try
            {
                using (POSDBEntities context = new POSDBEntities())
                {
                    Kategoriler kategoriler = new Kategoriler();
                    kategoriler.id = id;
                    context.Kategoriler.Attach(kategoriler);
                    context.Kategoriler.Remove(kategoriler);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;
        }


        public GenelModel update(Kategoriler kategori)
        {
            GenelModel model = new GenelModel();
            try
            {
                using (POSDBEntities context = new POSDBEntities())
                {

                    context.Kategoriler.Attach(kategori);
                    context.Entry(kategori).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;
        }

        public GenelModel update1(Kategoriler kategori) // yeni değerler kategori 
        {
            GenelModel model = new GenelModel();
            try
            {
                using (POSDBEntities context = new POSDBEntities())
                {
                    var kategorim = context.Kategoriler.Find(kategori.id);
                    kategorim.ad = kategori.ad;
                    kategorim.aktif = kategori.aktif;
                    kategorim.yeni1 = kategori.yeni1;
                    context.Entry(kategorim).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;
        }


        public GenelModel getList22() // yeni değerler kategori 
        {
            GenelModel model = new GenelModel();
            try
            {
                using (POSDBEntities context = new POSDBEntities())
                {
                    //GC.Collect();
                    string query = @"select Kategoriler.id,Kategoriler.ad,kategoriId,tarih,fiyat,aktif from Urunler
left join Kategoriler on Kategoriler.id=Urunler.kategoriId";
                    DataTable dataTable = getQueryToDataTableNew(query, context);
                    string json = JsonConvert.SerializeObject(dataTable);
                    List<KatUrunModel> modelim= JsonConvert.DeserializeObject<List<KatUrunModel>>(json);

                    model.data = modelim;
                }



            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;
        }


        [ApiExplorerSettings(IgnoreApi = true)] // yazılan metot swaggerde gözükmesin demek
        public DataTable getQueryToDataTableNew(string query, DbContext context)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = context.Database.Connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = 0;//sınırsız demek
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public GenelModel delete1(int id)
        {
            GenelModel model = new GenelModel();
            try
            {
                using (POSDBEntities context = new POSDBEntities())
                {
                    var kategorim = context.Kategoriler.Where(x => x.id == id).FirstOrDefault(); // varsa gelir yoksa null döner
                    if (kategorim != null)
                    {
                        context.Kategoriler.Remove(kategorim);
                        context.SaveChanges();
                    }
                    else
                    {
                        model.success = false;
                        model.message = "Veri tabanında böyle bir kayıt yokdur id= " + id;
                    }

                }

            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;
        }

        public GenelModel kategoriKaydet(Kategoriler kategori)
        {
            GenelModel model = new GenelModel();
            try
            {
                int a = Convert.ToInt32(kategori.yeni1);
                POSDBEntities context = new POSDBEntities();
                context.Kategoriler.Add(kategori);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }

            return model;

        }
        public string getAd(string ad)
        {
            return "ramazan " + ad;
        }

        public List<Sinif> getKategoriler()
        {

            List<Sinif> siniflar = new List<Sinif>();
            Sinif sinif = new Sinif();
            sinif.ad = "Ramazan";
            sinif.id = 1;

            siniflar.Add(sinif);


            Sinif sinif2 = new Sinif();
            sinif2.ad = "Mustafa";
            sinif2.id = 2;


            siniflar.Add(sinif2);

            return siniflar;
        }
    }
}