﻿namespace Services.Aplication.DTOs.Auth
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
