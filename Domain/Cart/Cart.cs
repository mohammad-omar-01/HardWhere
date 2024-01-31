﻿using Domain.UserNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.CartNS
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int cartId { get; set; }

        [ForeignKey(nameof(User))]
        public int userId { get; set; }
        public float total { get; set; } = 0;
        public bool isEmpty { get; set; } = true;
        public int carItemscount { get; set; }
        public List<CartProduct>? contents { get; set; } = new List<CartProduct> { };
    }
}
