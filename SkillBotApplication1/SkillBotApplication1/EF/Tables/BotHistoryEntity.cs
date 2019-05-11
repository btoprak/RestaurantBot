using System;
using System.ComponentModel.DataAnnotations;


namespace SkillBotApplication1.EF.Tables
{
    public class BotHistoryEntity
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }
        public string Source { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}