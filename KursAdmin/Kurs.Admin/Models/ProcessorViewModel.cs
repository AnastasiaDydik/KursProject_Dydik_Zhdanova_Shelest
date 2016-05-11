using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class ProcessorViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Количество ядер")]
        public int Cores { get; set; }

        [Display(Name = "Частота")]
        public int Frequency { get; set; }

        public ProcessorViewModel()
        {

        }

        public ProcessorViewModel(Processor processor)
        {
            if (processor == null)
                throw new ArgumentNullException(nameof(processor));

            Id = processor.Id;
            Title = processor.Title;
            Cores = processor.Cores;
            Frequency = processor.Frequency;
        }
    }
}