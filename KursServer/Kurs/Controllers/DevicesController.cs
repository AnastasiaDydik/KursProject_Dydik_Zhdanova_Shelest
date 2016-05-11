using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;

namespace Kurs.Controllers
{
    public class DevicesController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Devices
        public IQueryable<Kurs.Admin.Repository.Device> GetDevices(int? cat = null, decimal? minPrice = null, decimal? maxPrice = null, string keyword = null, bool? isActual = null )
        {
            var devices = db.Devices.AsQueryable();
            if(cat.HasValue)
                devices = devices.Where(it => it.CategoryId == cat.Value);
            if (minPrice.HasValue)
                devices = devices.Where(it => it.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                devices = devices.Where(it => it.Price <= maxPrice.Value);
            if (!string.IsNullOrWhiteSpace(keyword))
                devices = devices.Where(it => it.Model.Contains(keyword) || it.Info.Contains(keyword));
            if (isActual.HasValue)
                devices = devices.Where(it => it.FreeCount > 0);

            return devices.Select(it => new Kurs.Admin.Repository.Device
            {
                FreeCount = it.FreeCount,
                Heigth = it.Heigth,
                Id = it.Id,
                Image = it.Image,
                Info = it.Info,
                Memory = it.Memory,
                Model = it.Model,
                Price = it.Price,
                Ram = it.Ram,
                TotalCount = it.TotalCount,
                Width = it.Width,
                CategoryId = it.CategoryId,
                ColorId = it.ColorId,
                DigitalCameraId = it.DigitalCameraId,
                MakerId = it.MakerId,
                OperatingSystemId = it.OperatingSystemId,
                ProcessorId = it.ProcessorId,
                ScreenResolutionId = it.ScreenResolutionId,
                CountryId = it.CountryId,
                MaterialId = it.MaterialId
            });
        }

        // GET: api/Devices/5
        [ResponseType(typeof(Kurs.Admin.Repository.Device))]
        public IHttpActionResult GetDevice(int id)
        {
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return NotFound();
            }

            var model = new Kurs.Admin.Repository.Device
            {
                FreeCount = device.FreeCount,
                Heigth = device.Heigth,
                Id = device.Id,
                Image = device.Image,
                Info = device.Info,
                Memory = device.Memory,
                Model = device.Model,
                Price = device.Price,
                Ram = device.Ram,
                TotalCount = device.TotalCount,
                Width = device.Width,
                CategoryId = device.CategoryId,
                ColorId = device.ColorId,
                DigitalCameraId = device.DigitalCameraId,
                MakerId = device.MakerId,
                OperatingSystemId = device.OperatingSystemId,
                ProcessorId = device.ProcessorId,
                ScreenResolutionId = device.ScreenResolutionId,
                CountryId = device.CountryId,
                MaterialId = device.MaterialId,
            };

            return Ok(model);
        }

        // PUT: api/Devices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDevice(int id, Kurs.Admin.Repository.Device model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var device = db.Devices.Find(model.Id);
            device.CategoryId = model.CategoryId;
            device.ColorId = model.ColorId;
            device.CountryId = model.CountryId;
            device.DigitalCameraId = model.DigitalCameraId;
            device.FreeCount = model.FreeCount;
            device.Heigth = model.Heigth;
            device.Image = model.Image;
            device.Info = model.Info;
            device.MakerId = model.MakerId;
            device.MaterialId = model.MaterialId;
            device.Memory = model.Memory;
            device.Model = model.Model;
            device.OperatingSystemId = model.OperatingSystemId;
            device.Price = model.Price;
            device.ProcessorId = model.ProcessorId;
            device.Ram = model.Ram;
            device.ScreenResolutionId = model.ScreenResolutionId;
            device.TotalCount = model.TotalCount;
            device.Width = model.Width;
            
            db.Entry(device).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Devices
        [ResponseType(typeof(Kurs.Admin.Repository.Device))]
        public IHttpActionResult PostDevice(Kurs.Admin.Repository.Device model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var device = new Device
            {
                FreeCount = model.FreeCount,
                Heigth = model.Heigth,
                Id = model.Id,
                Image = model.Image,
                Info = model.Info,
                Memory = model.Memory,
                Model = model.Model,
                Price = model.Price,
                Ram = model.Ram,
                TotalCount = model.TotalCount,
                Width = model.Width,
                CategoryId = model.CategoryId,
                ColorId = model.ColorId,
                DigitalCameraId = model.DigitalCameraId,
                MakerId = model.MakerId,
                OperatingSystemId = model.OperatingSystemId,
                ProcessorId = model.ProcessorId,
                ScreenResolutionId = model.ScreenResolutionId,
                CountryId = model.CountryId,
                MaterialId = model.MaterialId
            };

            db.Devices.Add(device);
            db.SaveChanges();
            model.Id = device.Id;
            return CreatedAtRoute("DefaultApi", new { id = device.Id }, model);
        }

        // DELETE: api/Devices/5
        [ResponseType(typeof(Kurs.Admin.Repository.Device))]
        public IHttpActionResult DeleteDevice(int id)
        {
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return NotFound();
            }

            var model = new Kurs.Admin.Repository.Device
            {
                FreeCount = device.FreeCount,
                Heigth = device.Heigth,
                Id = device.Id,
                Image = device.Image,
                Info = device.Info,
                Memory = device.Memory,
                Model = device.Model,
                Price = device.Price,
                Ram = device.Ram,
                TotalCount = device.TotalCount,
                Width = device.Width,
                CategoryId = device.CategoryId,
                ColorId = device.ColorId,
                DigitalCameraId = device.DigitalCameraId,
                MakerId = device.MakerId,
                OperatingSystemId = device.OperatingSystemId,
                ProcessorId = device.ProcessorId,
                ScreenResolutionId = device.ScreenResolutionId,
                CountryId = device.CountryId,
                MaterialId = device.MaterialId
            };


            db.Devices.Remove(device);
            db.SaveChanges();

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceExists(int id)
        {
            return db.Devices.Count(e => e.Id == id) > 0;
        }
    }
}