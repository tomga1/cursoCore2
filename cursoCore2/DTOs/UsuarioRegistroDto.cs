﻿using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.DTOs
{
    public class UsuarioRegistroDto

    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Role { get; set; }

    }
}
