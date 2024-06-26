﻿namespace DnDEZBackend.Models.DTOs.CharacterDTOs
{
    public class CharacterPutDTO
    {
        public int UserId { get; set; }

        public string? Name { get; set; } = null!;

        public string? Race { get; set; } = null!;

        public string? Class { get; set; } = null!;

        public int? Level { get; set; }
        public int ProfBonus { get; set; }

        public int Initiative { get; set; }

        public int Speed { get; set; }

        public string? Personality { get; set; }

        public string? Ideals { get; set; }

        public string? Bonds { get; set; }

        public string? Flaws { get; set; }

        public int HitDie { get; set; }

        public int Hp { get; set; }

        public string? Alignment { get; set; } = null!;

        public string? CharAbilityScores { get; set; } = null!;

        public string CharSkillScores { get; set; } = null!;

        public string CharSavingThrows { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}
