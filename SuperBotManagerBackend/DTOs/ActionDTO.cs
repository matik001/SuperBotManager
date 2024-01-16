﻿using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionUpdateDTO
    {
        public int Id { get; set; }

        public ActionSchema ActionData { get; set; }
    }
    public class ActionDTO : ActionUpdateDTO
    {
        public ActionStatus ActionStatus { get; set; } = ActionStatus.Pending;
        public int ActionExecutorId { get; set; }

        public int? ErrorId { get; set; }
        /// TODO Error object

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
