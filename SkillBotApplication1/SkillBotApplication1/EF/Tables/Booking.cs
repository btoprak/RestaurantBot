using System;
using System.ComponentModel.DataAnnotations;


namespace SkillBotApplication1.EF.Tables
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumPeople { get; set; }
        public string PhNum { get; set; }
        public string Requests { get; set; }
        public DateTime BookingDateTime { get; set; }
    }
}