﻿namespace DnDEZBackend.Models.DTOs.CharacterDTOs
{
    public class CharacterPutDTO
    {
        public int UserId { get; set; }

        public string? Name { get; set; } = null!;

        public string? Race { get; set; } = null!;

        public string? Class { get; set; } = null!;

        public int? Level { get; set; }

        public string? CharAbilityScores { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}
