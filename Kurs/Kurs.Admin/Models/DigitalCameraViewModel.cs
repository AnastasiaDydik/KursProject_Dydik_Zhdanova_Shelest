using Kurs.Admin.Repository;
using System;

namespace Kurs.Admin.Models
{
    public class DigitalCameraViewModel
    {
        public int Id { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public DigitalCameraViewModel()
        {

        }

        public DigitalCameraViewModel(DigitalCamera camera)
        {
            if (camera == null)
                throw new ArgumentNullException(nameof(camera));

            Id = camera.Id;
            Height = camera.Height;
            Width = camera.Width;
        }
    }
}