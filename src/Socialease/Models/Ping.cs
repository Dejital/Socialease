﻿using System;

namespace Socialease.Models
{
    public class Ping
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Occurred { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public int PersonId { get; set; }
        public int PingTypeId { get; set; }
    }
}