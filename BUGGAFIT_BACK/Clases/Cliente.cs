namespace BUGGAFIT_BACK.Clases
{
    public class Cliente
    {

            public string CLI_ID { get; set; }
            public string? CLI_NOMBRE { get; set; }
            public string? CLI_TIPOCLIENTE { get; set; }
            public string? CLI_UBICACION { get; set; }
            public string? CLI_DIRECCION { get; set; }
            public int? CLI_TELEFONO { get; set; }
            public string? CLI_CORREO { get; set; }
            public DateTime CLI_FECHACREACION { get; set; }
            public bool CLI_ESTADO { get; set; }
            public bool? CLI_ESCREDITO { get; set; }

    }    
}
