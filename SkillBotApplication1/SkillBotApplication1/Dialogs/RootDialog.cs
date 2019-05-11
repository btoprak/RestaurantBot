using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SkillBotApplication1.EF;

namespace SkillBotApplication1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(None);
        }

        public virtual async Task None(IDialogContext context, IAwaitable<object> result)
        {
                var reply = context.MakeMessage();
                var activity = await result as Activity;
                activity.AsTypingActivity();
            if (Regex.IsMatch(activity.Text, @"\b(selam|merhaba|meraba|mrb|slm|s.a|sa|selamun aleykum)\b", RegexOptions.IgnoreCase))
            {
                await context.PostAsync("Hoşgeldiniz nasıl yardımcı olabilirim?");
            }
            else if (Regex.IsMatch(activity.Text, @"\b(teşekkürler|teşekkür ederim|eyvallah|eyv)\b", RegexOptions.IgnoreCase))
            {
                await context.PostAsync("Rica Ederim.");
            }
            else if (Regex.IsMatch(activity.Text, @"\b(randevu)\b", RegexOptions.IgnoreCase))
            {
                MakeBooking(context);
                using (DataContext db = new DataContext())
                {
                    db.BotHistories.Add(new EF.Tables.BotHistoryEntity()
                    {
                        UserId = activity.From.Id,
                        Source = activity.ChannelId
                    });
                    try
                    {
                        db.SaveChanges();
                    }
                    catch { }
                }
            }
            else if (Regex.IsMatch(activity.Text, @"\b(adres|adresiniz|lokasyon|yer|yeriniz)\b", RegexOptions.IgnoreCase))
            {
                 await GetLocation(context);
                
            }
            else
            {
                await context.PostAsync("Hmm Ne istediğinizden emin değilim, üzgünüm!");
            }
            // Calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            // Return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

        }

        public async Task GetLocation(IDialogContext context)
        {
            try
            {
                await context.PostAsync("Adresimiz:");
                await context.PostAsync("Beyazevler Mahallesi, 80003 Sokak No:14/A Çukurova, 01150 Çukurova/Adana");
                var reply = context.MakeMessage();
                reply.Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            ContentUrl = "https://dev.virtualearth.net/REST/V1/Imagery/Map/Road?form=BTCTRL&mapArea=37.02944,35.31372,37.03343,35.31896&mapSize=500,280&pp=37.03135,35.31652;1;1&dpi=1&logo=always&key=Am0D5Lvq44f-wpvwGKLfcOlHCirwmAd3ynRH8Yuh0_hV063ZaKOTNlwFAr3b1fp6",
                            ContentType = "image/jpg",
                            Name = "Map.jpg"
                            
                        }
                    };

                await context.PostAsync(reply);
            }
            catch (Exception)
            {
                await context.PostAsync("Something went wrong, sorry :(");
            }
            finally
            {
            }
        }

        private void MakeBooking(IDialogContext context)
        {
            //PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { FlightsOption, HotelsOption }, "Are you looking for a flight or a hotel?", "Not a valid option", 3);

            context.Call(new BookingDialog(),this.ResumeAfterBookingDialog);
        }

        private async Task ResumeAfterBookingDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(None);
            }
        }
    }
}