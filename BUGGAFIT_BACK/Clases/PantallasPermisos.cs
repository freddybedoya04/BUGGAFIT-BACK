namespace BUGGAFIT_BACK.Clases
{
    public class PantallasPermisos
    {
        public string PAN_CODIGO { get; set; }
        public string PAN_NOMBRE { get; set; }
        public string PAN_PATH { get; set; }
        public string PAN_ICON { get; set; }
        public string PAN_TEXT { get; set; }
        public DateTime PAN_TIMESPAN { get; set; }
        public bool PAN_ESTADO { get; set; }

        public bool PPP_VER { get; set; }
        public bool PPP_AGREGAR { get; set; }
        public bool PPP_EDITAR { get; set; }
        public bool PPP_ELIMINAR { get; set; }
    }
}
