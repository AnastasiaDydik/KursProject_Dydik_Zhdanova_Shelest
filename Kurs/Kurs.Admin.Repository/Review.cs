namespace Kurs.Admin.Repository
{
    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int DeviceId { get; set; }

        /*public DeviceData Device
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
        }*/
    }
}
