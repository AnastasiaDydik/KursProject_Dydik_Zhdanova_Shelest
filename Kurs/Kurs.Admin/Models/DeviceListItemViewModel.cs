using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class DeviceListItemViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Display(Name = "Высота")]
        public Nullable<double> Heigth { get; set; }

        [Display(Name = "Длина")]
        public Nullable<double> Width { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Price { get; set; }

        [Display(Name = "Оперативная память")]
        public Nullable<int> Ram { get; set; }

        [Display(Name = "Встроеная память")]
        public Nullable<int> Memory { get; set; }

        [Display(Name = "Описание")]
        public string Info { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Общее количество")]
        public int TotalCount { get; set; }

        [Display(Name = "Осталось")]
        public int FreeCount { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        [Display(Name = "Производитель")]
        public string Maker { get; set; }

        [Display(Name = "Разрешение экрана")]
        public string ScreenResolution { get; set; }

        [Display(Name = "Процессор")]
        public string Processor { get; set; }

        [Display(Name = "Операционная система")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Камера")]
        public string DigitalCamera { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Материал")]
        public string Material { get; set; }

        public DeviceListItemViewModel()
        {

        }

        public DeviceListItemViewModel(Device device,
            Category category = null,
            Color color = null,
            Maker maker = null,
            ScreenResolution screenResolution = null,
            Processor proccessor = null,
            Repository.OperatingSystem os = null,
            DigitalCamera camera = null,
            Country country = null,
            string material = null)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            Id = device.Id;
            Model = device.Model;
            Heigth = device.Heigth;
            Width = device.Width;
            Price = device.Price;
            Ram = device.Ram;
            Memory = device.Memory;
            Info = device.Info;
            Image = device.Image;
            TotalCount = device.TotalCount;
            FreeCount = device.FreeCount;

            if (category != null)
                Category = category.Title;

            if (color != null)
                Color = color.Title;

            if (maker != null)
                Maker = maker.Title;

            if (screenResolution != null)
                ScreenResolution = $"{screenResolution.Width}x{screenResolution.Height} {screenResolution.Diagonal}\"";

            if (proccessor != null)
                Processor = proccessor.Title;

            if (os != null)
                OperatingSystem = os.Title;

            if (camera != null)
                DigitalCamera = $"{camera.Width}x{camera.Height}";

            if (country != null)
                Country = country.Title;

            Material = material;

        }
    }

    public class DeviceViewModel
    {
        [Display(Name = "Id")]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Модель")]
        [Required]
        [DataType(DataType.Text)]
        public string ModelName { get; set; }

        [Display(Name = "Высота")]
        public double? Heigth { get; set; }

        [Display(Name = "Длина")]
        public double? Width { get; set; }

        [Display(Name = "Стоимость")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "ОЗУ")]
        public int? Ram { get; set; }

        [Display(Name = "Встроеная память")]
        public int? Memory { get; set; }

        [Display(Name = "Описание")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        [Display(Name = "Изображение")]
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Display(Name = "Всего, шт.")]
        [Required]
        public int TotalCount { get; set; }

        [Display(Name = "Осталось, шт")]
        [Required]
        public int FreeCount { get; set; }

        [Display(Name = "Кстегория")]
        public int CategoryId { get; set; }

        [Display(Name = "Цвет")]
        public int? ColorId { get; set; }

        [Display(Name = "Производитель")]
        [Required]
        public int MakerId { get; set; }

        [Display(Name = "Разрешение экрана")]
        public int? ScreenResolutionId { get; set; }

        [Display(Name = "Процессор")]
        public int? ProcessorId { get; set; }

        [Display(Name = "Опреационная система")]
        public int? OperatingSystemId { get; set; }

        [Display(Name = "Камера")]
        public int? DigitalCameraId { get; set; }

        [Display(Name = "Страна")]
        [Required]
        public int CountryId { get; set; }
        
    }
}