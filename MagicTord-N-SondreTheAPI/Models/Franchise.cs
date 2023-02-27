﻿using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheAPI.Models
{
    public class Franchise
    {
        [Key]
        public int FranchiseID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Description { get; set; }


        public virtual ICollection<Movie> Movies { get; set; }
    }
}
