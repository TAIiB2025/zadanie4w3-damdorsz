﻿namespace WebAPI.DTOs
{
    public class FilmBodyDto
    {
        public string Tytul { get; set; } = string.Empty;
        public string Rezyser { get; set; } = string.Empty;
        public string Gatunek { get; set; } = string.Empty;
        public int RokWydania { get; set; }
    }
}
