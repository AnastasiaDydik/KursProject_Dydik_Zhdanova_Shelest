using Kurs.Storage;
using System;

namespace Kurs.Model.Data
{
    public class ConsultantData
    {
        public int Id
        {
            get { return Consultant.Id; }
            set { Consultant.Id = value; }
        }

        public string Name
        {
            get { return Consultant.Name; }
            set { Consultant.Name = value; }
        }

        public string PhoneNumber
        {
            get { return Consultant.PhoneNumber; }
            set { Consultant.PhoneNumber = value; }
        }

        public string Email
        {
            get { return Consultant.Email; }
            set { Consultant.Email = value; }
        }

        internal Consultant Consultant { get; set; }

        bool IncludeDependency { get; set; }

        public ConsultantData()
        {
            Consultant = new Consultant();
            IncludeDependency = true;
        }

        internal ConsultantData(Consultant consultant, bool includeDependency = true)
        {
            if (consultant == null)
                throw new ArgumentNullException(nameof(consultant));

            Consultant = consultant;
            IncludeDependency = includeDependency;
        }
    }
}
