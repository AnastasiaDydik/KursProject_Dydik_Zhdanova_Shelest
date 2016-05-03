using Kurs.Storage;
using System;

namespace Kurs.Model.Data
{
    public class ReviewData
    {
        public int Id
        {
            get { return Review.Id; }
            set { Review.Id = value; }
        }

        public string Content
        {
            get { return Review.Content; }
            set { Review.Content = value; }
        }

        public DeviceData Device
        {
            get { return IncludeDependency && Review != null && Review.Device != null ? new DeviceData(Review.Device, false) : null; }
            set { Review.Device = value.Device; }
        }


        internal Review Review { get; set; }

        bool IncludeDependency { get; set; }

        public ReviewData()
        {
            Review = new Review();
            IncludeDependency = true;
        }

        internal ReviewData(Review review, bool includeDependency = true)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            Review = review;
            IncludeDependency = includeDependency;
        }
    }
}
