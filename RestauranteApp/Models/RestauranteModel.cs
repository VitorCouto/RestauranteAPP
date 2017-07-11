namespace RestauranteApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Common;

    public partial class RestauranteModel : DbContext
    {
        public RestauranteModel() : base("name=RestauranteModel")
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public RestauranteModel(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public virtual DbSet<Restaurante> Restaurante { get; set; }
        public virtual DbSet<Pratos> Pratos { get; set; }

    }

    [Table("tbl_restaurante")]
    public class Restaurante
    {
        [Column("cod_restaurante"), Key]
        public int restauranteId { get; set; }

        [Column("name_restaurante"), MaxLength(80), Required]
        public string nameRestaurante { get; set; }

    }

    [Table("tbl_pratos")]
    public class Pratos
    {
        [Column("cod_prato"), Key]
        public int pratoId { get; set; }

        [Column("cod_restaurante"), ForeignKey("restaurante"), Required]
        public int restauranteId { get; set; }

        public virtual Restaurante restaurante { get; set; }

        [Column("name_prato"), MaxLength(80), Required]
        public string namePrato { get; set; }

        [Column("vlr_prato"), Required]
        public decimal valor { get; set; }

    }
}
