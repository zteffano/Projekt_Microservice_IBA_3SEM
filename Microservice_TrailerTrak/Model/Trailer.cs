using System;

namespace Microservice_TrailerTrak.Model
{

    // Trailer Model udkast
    // Tænker måske vi skal ændre det hele til engelsk, just in case
    public class Trailer
    {
        public int ID { get; set; }
        public string Navn { get; set; }
        public int Årgang { get; set; }
        public string Mærke { get; set; }
        public int Vægt { get; set; }
        public int MaxVægt { get; set; }
        public bool BookedStatus { get; set; }
        public DateTime? BookedIndtil { get; set; }
        public decimal PrisDag { get; set; }
        public decimal PrisUge { get; set; }
        public decimal PrisMåned { get; set; }

        public Trailer(int iD, string navn, int årgang, string mærke, int vægt, int maxVægt, bool bookedStatus, DateTime? bookedIndtil, decimal prisDag, decimal prisUge, decimal prisMåned)
        {
            ID = iD;
            Navn = navn;
            Årgang = årgang;
            Mærke = mærke;
            Vægt = vægt;
            MaxVægt = maxVægt;
            BookedStatus = bookedStatus;
            BookedIndtil = bookedIndtil;
            PrisDag = prisDag;
            PrisUge = prisUge;
            PrisMåned = prisMåned;
        }
    }
}
