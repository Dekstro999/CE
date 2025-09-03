namespace Entidades.DTO.PlanesDeEstudio.Carreras
{
    public class CarreraDTO
    {
        public int IdCarrera { get; set; }
        public string ClaveCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public string AliasCarrera { get; set; }
        public int IdCordinador { get; set; }
        public bool EstadoCarrera { get; set; }
    }
}
