using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class CategoryData
    {
        public int Id
        {
            get { return Category.Id; }
            set { Category.Id = value; }
        }

        public string Title
        {
            get { return Category.Title; }
            set { Category.Title = value; }
        }

        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? Category?.Devices.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal Category Category { get; set; }

        bool IncludeDependency { get; set; }

        public CategoryData()
        {
            Category = new Category();
        }

        internal CategoryData(Category category, bool includeDependency = true)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            Category = category;
            IncludeDependency = includeDependency;
        }
    }
}
