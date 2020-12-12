﻿using System;
using FS.Utils.Enums;

namespace FS.Data.Entities
{
    public sealed class Moviment : Entity
    {        
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        public EMovementCategory Category { get; set; }
        public EMovementType Type { get;  set; }
    }
}