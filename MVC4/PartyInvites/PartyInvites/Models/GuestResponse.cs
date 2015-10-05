﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;                    //атрибуты валидации

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Please, enter your name")]    //атрибуты валидации
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter your phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please specify whether you'll attend")]
        public bool? WillAttend { get; set; }
    }
}