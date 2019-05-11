using Microsoft.Bot.Builder.FormFlow;
using System;

namespace SkillBotApplication1
{
    [Serializable]
    public class BookingQuery
    {
        [Prompt("Ne zamana randevu almak istersiniz?")]
        public DateTime Date { get; set; }
        [Prompt("Hangi saati tercih edersiniz? Lütfen bu formatta giriniz; HH:mm  örneğin öğleden sonraysa 22:30")]
        public DateTime Time { get; set; }
        [Prompt("Adınız?")]
        public string Name { get; set; }
        [Prompt("Kaç kişi gelmeyi düşünüyorsunuz?")]
        public int NumPeople { get; set; }
        [Prompt("Lütfen telefon numararsı giriniz? Ex: 5xx xxx xxxx")]
        [Pattern(@"^(\d{3})\s(\d{3})\s(\d{4})$")]
        public string PhNum { get; set; }
        [Prompt("Herhangi bir notunuz var mı? Notunuz yoksa sadece hayır yazabilirsiniz!")]
        [Optional]
        public string Requests { get; set; }
    }
}