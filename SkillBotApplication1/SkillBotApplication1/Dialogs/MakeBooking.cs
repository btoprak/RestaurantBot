using Microsoft.Bot.Builder.FormFlow;
using System;
using Microsoft.Bot.Builder.Dialogs;
using SkillBotApplication1.EF.Tables;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SkillBotApplication1.Dialogs
{
    [Serializable]
    public class MakeBooking
    {
        [Prompt("What date would you like to make a booking for?")]
        public DateTime Date { get; set; }
        // Don't forget to make datetime nullable if it's optional!
        [Prompt("What is your preferred time? Please use HH:mm format e.g. 10.30pm would be 22:30")]
        //[Optional]
        public DateTime Time { get; set; }
        [Prompt("Your name?")]
        public string Name { get; set; }
        [Prompt("How many people will there be?")]
        public int NumPeople { get; set; }
        [Prompt("Can I get your phone number please?")]
        public string PhNum { get; set; }
        [Prompt("Enter any additional requests or notes. Say None if you don't have any.")]
        [Optional]
        public string Requests { get; set; }

        public static IForm<MakeBooking> BuildForm()
        {
            async Task tamamlandi(IDialogContext context, MakeBooking state)
            {
                await context.PostAsync("Randevunuz Alınmıştır teşekkür ederiz.");

                var booking = new Booking()
                {
                    Name = state.Name,
                    BookingDateTime = new DateTime(state.Date.Year, state.Date.Month, state.Date.Day, Convert.ToDateTime(state.Time).Hour, Convert.ToDateTime(state.Time).Minute, 0),
                    NumPeople = state.NumPeople,
                    PhNum = state.PhNum,
                };

                try
                {
                    using (EF.DataContext db = new EF.DataContext())
                    {

                        db.Bookings.Add(booking);

                        db.SaveChanges();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return new FormBuilder<MakeBooking>()
                .Message("Hoşgeldiniz.")

                .Field(nameof(Date))
                .Field(nameof(Time), active: IsTimeAdded, validate: ValidateTime)
                 .AddRemainingFields()
                .Field(nameof(Name))
                .Field(nameof(NumPeople))
                .Field(nameof(PhNum), validate:ValidatePhNum)
                .Confirm("Confirm booking on {Date:d} at {Time:t}? (Y/N)")
                .OnCompletion(tamamlandi)
                .Build();
        }

        private static Task<ValidateResult> ValidateTime(MakeBooking state, object response)
        {
            var result = new ValidateResult();
            var dt = (DateTime)response;
            // Do the checks here whether the time is available. 
            // Hard coded for demo purposes
            if (dt.ToString("HH:mm") == "20:30")
            {
                // If time not available
                result.IsValid = false;
                result.Feedback = "Sorry, that time is not available! Times that are available are: 6.30pm, 7.00pm, 8.00pm, 9.00pm";
            }
            else
            {
                result.IsValid = true;
                result.Value = response;
            }
            return Task.FromResult(result);
        }
        private static bool IsTimeAdded(MakeBooking state)
        {
            if (state.Date.TimeOfDay.TotalSeconds == 0)
            {
                return true;
            }
            return false;
        }
        private static Task<ValidateResult> ValidatePhNum(MakeBooking state, object response)
        {
            var result = new ValidateResult();
            string phoneNumber = string.Empty;
            if (IsPhNum((string)response))
            {
                result.IsValid = true;
                result.Value = response;
            }
            else
            {
                result.IsValid = false;
                result.Feedback = "Make sure the phone number you entered are all numbers.";
            }
            return Task.FromResult(result);
        }
        private static bool IsPhNum(string response)
        {
            if (Regex.IsMatch(response, @"^\d+$"))
            {
                return true;
            }
            return false;
        }
    }
}