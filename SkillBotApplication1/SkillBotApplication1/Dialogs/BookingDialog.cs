using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using SkillBotApplication1.EF.Tables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillBotApplication1.Dialogs
{
    [Serializable]
    public class BookingDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var bookingFormDialog = FormDialog.FromForm(BookingBuildForm, FormOptions.PromptInStart);
            context.Call(bookingFormDialog, OnBookingCompleted);
        }

        private static IForm<BookingQuery> BookingBuildForm()
        {
            async Task tamamlandi(IDialogContext context2, BookingQuery state)
            {
                await context2.PostAsync("Randevunuz Alınmıştır teşekkür ederiz.");
               
                var booking = new Booking()   //database yazdır
                {
                    Name = state.Name,
                    BookingDateTime = new DateTime(state.Date.Year, state.Date.Month, state.Date.Day, Convert.ToDateTime(state.Time).Hour, Convert.ToDateTime(state.Time).Minute, 0),
                    NumPeople = state.NumPeople,
                    PhNum = state.PhNum,
                    Requests = state.Requests,
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

                var randevutime = state.Time.Hour + ":" + state.Time.Minute;
                var reply = context2.MakeMessage();

                ReceiptCard receiptCard = new ReceiptCard() //randevu bilgileri card
                {
                    Title = state.Name,
                    Facts = new List<Fact> {
                        new Fact("Telefon Num.",state.PhNum) ,
                        new Fact("Rezervasyon gününüz", Convert.ToString(state.Date)),
                        new Fact("Rezarvasyon saatiniz",Convert.ToString(randevutime)),
                        new Fact("Kişi Sayısı", Convert.ToString(state.NumPeople)),

                    }
                };

                Attachment plAttachment = receiptCard.ToAttachment();
                reply.Attachments.Add(plAttachment);

                await context2.PostAsync(reply);


            }
            return new FormBuilder<BookingQuery>()   //senaryo sırası 
                 .Message("Hoşgeldiniz.")

                 .Field(nameof(BookingQuery.Date))
                 .Field(nameof(BookingQuery.Time))
                  .AddRemainingFields()
                 .Field(nameof(BookingQuery.Name))
                 .Field(nameof(BookingQuery.NumPeople))
                 .Field(nameof(BookingQuery.PhNum))
                 .Confirm("Randevunuzu onaylıyor musunuz {Date:d} saat {Time:t}? (Y/N)")
                 .OnCompletion(tamamlandi)
                 .Build();
        }

        private async Task OnBookingCompleted(IDialogContext context, IAwaitable<BookingQuery> result)
        {
             context.Done<object>(null);
        }
    }
}