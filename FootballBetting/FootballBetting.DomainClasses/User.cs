﻿using System.Collections.Generic;

namespace FootballBetting.DomainClasses
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string  FullName { get; set; }

        public string Balance { get; set; }

        public List<Bet> Bets { get; set; } = new List<Bet>();
    }
}