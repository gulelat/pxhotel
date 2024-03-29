﻿using System;

namespace PX.Business.Models.SQLTool
{
    public class SQLCommandHistoryModel : BaseModel
    {
        public int Id { get; set; }
        /// <summary>
        /// SQL statement that has been sent
        /// </summary>
        public string Query { get; set; }
    }
}