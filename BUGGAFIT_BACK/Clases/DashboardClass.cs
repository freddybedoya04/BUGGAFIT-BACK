﻿namespace BUGGAFIT_BACK.Clases
{
    public class DashboardClass
    {
        public DashboardClass()
        {
            DatosCards = new();
            DatosGraficas = new();
        }
        public DashboardClass(DatosGraficas datosGraficas, DatosCards datosCards)
        {
            DatosCards = datosCards;
            DatosGraficas = datosGraficas;
        }

        public DatosCards DatosCards { get; set; }
        public DatosGraficas DatosGraficas { get; set; }
    }

    public class DatosCards
    {
        public double SumaVentas { get; set; } = 0;
        public double SumaGastos { get; set; } = 0;
        public double SumaCompras { get; set; } = 0;
        public double SumaCreditos { get; set; } = 0;
        public double SumaDeudas { get; set; } = 0;

    }
    public class DatosGraficas
    {
        public List<ListaProductosVendidos> ProductosMasVendidos { get; set; } = new();
        public List<Ventas> VentasRealizadas { get; set; } = new();
        public List<IngresosCuentas> IngresosCuentas { get; set; } = new();

    }

    public class ListaProductosVendidos
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int CantidadProducto { get; set; }
    }

    public class IngresosCuentas
    {
        public int Codigo { get; set; }
        public string? Nombre { get; set; }
        public double IngresosTotales { get; set; }
    }
}